using FluentValidation;
using ShopProject.Application;
using ShopProject.Application.DataTransfer;
using ShopProject.DataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopProject.Implementation.Validators
{
    public class CreateOrderValidator : AbstractValidator<OrderDto>
    {
        private readonly ShopProjectContext _context;
        private readonly IApplicationActor _actor;
        public CreateOrderValidator(ShopProjectContext context, IApplicationActor actor)
        {
            _context = context;
            _actor = actor;

            RuleFor(x => x)
                .Must(_ => _context.Users.Any(u => u.Id == _actor.Id))
                .WithMessage("User does not exist.");

            RuleFor(x => x.AddressId)
                .Must(id => _context.Addresses.Any(a => a.Id == id && a.UserId == _actor.Id))
                .WithMessage("Address does not exist or does not belong to this user.");

            RuleFor(x => x.ShippingMethodId)
                .Must(id => _context.ShippingMethods.Any(s => s.Id == id))
                .WithMessage("Shipping method does not exist.");

            RuleFor(x => x.CouponId)
                .Must(id => !id.HasValue || _context.Coupons.Any(c => c.Id == id.Value))
                .WithMessage("Coupon does not exist.");

            RuleFor(x => x.CouponId)
                .Must(id => _context.Coupons.Any(c => c.Id == id!.Value && c.ExpiryDate >= DateTime.Now))
                .WithMessage("Coupon has expired.")
                .When(x => x.CouponId.HasValue);
        }
    }
}
