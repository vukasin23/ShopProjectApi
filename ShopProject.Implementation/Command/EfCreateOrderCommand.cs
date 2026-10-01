using FluentValidation;
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
            var cart = _context.Carts.Include(c=>c.CartItems).ThenInclude(ci=>ci.Product).Where(c => c.UserId == _actor.Id).FirstOrDefault();
            if (cart == null && !cart.CartItems.Any())
            {
                throw new ValidationException("Cart not found");
            }
            var transaction = _context.Database.BeginTransaction();
            var order = new Domain.Order
            {
                UserId = _actor.Id,
                AddressId = request.AddressId,
                ShippingMethodId = request.ShippingMethodId,
                CouponId = request.CouponId,
                OrderDate = DateTime.Now,
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
    }
}
