using FluentValidation;
using FluentValidation.Results;
using ShopProject.Application;
using ShopProject.Application.Command;
using ShopProject.Application.DataTransfer;
using ShopProject.DataAccess;
using ShopProject.Implementation.Validators;
using System;
using Microsoft.EntityFrameworkCore;
using ShopProject.Application.Exceptions;
using ShopProject.Domain;

namespace ShopProject.Implementation.Command
{
    public class EfCreateOrderCommand : ICreateOrderCommand
    {
        private readonly ShopProjectContext _context;
        private readonly CreateOrderValidator _validator;
        private readonly IApplicationActor _actor;

        public int Id => 15;
        public string Name => "Create order command";
        public EfCreateOrderCommand(ShopProjectContext context, CreateOrderValidator validator, IApplicationActor actor)
        {
            _context = context;
            _validator = validator;
            _actor = actor;
        }

        public void Execute(OrderDto request)
        {
            _validator.ValidateAndThrow(request);
            // Query filters are ignored so soft-deleted products are loaded too. Otherwise the filter would silently drop those cart items.
            var cart = _context.Carts.IgnoreQueryFilters().Include(c=>c.CartItems).ThenInclude(ci=>ci.Product).ThenInclude(p => p.Inventories).Where(c => c.UserId == _actor.Id).FirstOrDefault();

            if (cart == null)
            {
                throw new ValidationException("Cart not found");
            }

            if (!cart.CartItems.Any())
            {
                throw new ValidationException("Cart is empty");
            }

            var unavailableItems = cart.CartItems.Where(ci => ci.Product.IsDeleted).ToList();
            if (unavailableItems.Any())
            {
                throw new ValidationException(unavailableItems.Select(ci =>
                    new ValidationFailure("CartItems", $"Product '{ci.Product.Name}' is no longer available. Remove cart item {ci.Id} from the cart.")));
            }

            var coupon = _context.Coupons.FirstOrDefault(c => c.Id == request.CouponId);
            var shippingMethodPrice = _context.ShippingMethods.Where(s => s.Id == request.ShippingMethodId).Select(s=>s.Price).FirstOrDefault();
           
            var now = DateTime.Now;

            // Check all items first, so nothing is deducted when any product is out of stock.
            var outOfStock = cart.CartItems
                .Where(ci => ci.Product.Inventories.Sum(i => i.Quantity) < ci.Quantity)
                .ToList();
            if (outOfStock.Any())
            {
                throw new ValidationException(outOfStock.Select(ci =>
                    new ValidationFailure("CartItems", $"Not enough stock for product '{ci.Product.Name}'.")));
            }

            foreach (var item in cart.CartItems)
            {
                DeductStock(item.Product.Inventories, item.Quantity, now);
            }

            var order = new Domain.Order
            {
                UserId = _actor.Id,
                AddressId = request.AddressId,
                ShippingMethodId = request.ShippingMethodId,
                CouponId = request.CouponId,
                OrderDate = now,
                TotalPrice = CalculateTotal(cart.CartItems, coupon, shippingMethodPrice),
                OrderLines = cart.CartItems.Select(ci=> new OrderLine()
                {
                    ProductId = ci.ProductId,
                    Quantity = ci.Quantity,
                    UnitPrice = ci.Product.Price

                }).ToList()

            };

            _context.Orders.Add(order);
      
            _context.CartItems.RemoveRange(cart.CartItems);
            _context.SaveChanges();

        }

        // Takes the quantity from the stores with the most stock first, so one order can be fulfilled from several stores.
        private static void DeductStock(IEnumerable<Inventory> inventories, int quantity, DateTime now)
        {
            var remaining = quantity;

            foreach (var inventory in inventories.OrderByDescending(i => i.Quantity))
            {
                if (remaining == 0)
                {
                    break;
                }

                var taken = Math.Min(inventory.Quantity, remaining);
                if (taken == 0)
                {
                    continue;
                }

                inventory.Quantity -= taken;
                inventory.LastUpdated = now;
                remaining -= taken;
            }
        }

        // The coupon discounts only the products, shipping is added at full price.
        private static decimal CalculateTotal(IEnumerable<CartItem> items, Coupon? coupon, decimal shippingPrice)
        {
            var productsTotal = items.Sum(i => i.Product.Price * i.Quantity);

            if (coupon != null)
            {
                productsTotal *= 1 - coupon.DiscountAmount / 100;
            }

            return productsTotal + shippingPrice;
        }
    }
}
