using FluentValidation;
using ShopProject.Application;
using ShopProject.Application.Command;
using ShopProject.Application.DataTransfer;
using ShopProject.DataAccess;
using ShopProject.Implementation.Validators;
using System;

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

            var order = new Domain.Order
            {
                UserId = _actor.Id,
                AddressId = request.AddressId,
                ShippingMethodId = request.ShippingMethodId,
                CouponId = request.CouponId,
                OrderDate = DateTime.Now
            };

            _context.Orders.Add(order);
            _context.SaveChanges();
        }
    }
}
