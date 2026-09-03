using FluentValidation;
using ShopProject.Application.Command;
using ShopProject.Application.DataTransfer;
using ShopProject.DataAccess;
using ShopProject.Implementation.Validators;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopProject.Implementation.Command
{
    public class EfCreateCouponCommand : ICreateCouponCommand
    {
        private readonly ShopProjectContext _context;
        private readonly CreateCouponValidator _validator;

        public EfCreateCouponCommand(ShopProjectContext context, CreateCouponValidator validator)
        {
            _context = context;
            _validator = validator;
        }

        public int Id => 7;

        public string Name => "Create new coupon";

        public void Execute(CouponDto request)
        {
            _validator.ValidateAndThrow(request);

            var coupon = new Domain.Coupon
            {
                Code = request.Code,
                DiscountAmount = request.DiscountAmount,
                ExpiryDate = request.ExpiryDate
            };

            _context.Coupons.Add(coupon);
            _context.SaveChanges();
        }
    }
}
