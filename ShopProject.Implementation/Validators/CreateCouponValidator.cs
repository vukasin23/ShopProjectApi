using FluentValidation;
using ShopProject.Application.DataTransfer;
using ShopProject.DataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopProject.Implementation.Validators
{
    public class CreateCouponValidator:AbstractValidator<CouponDto>
    {
        public CreateCouponValidator(ShopProjectContext context)
        {
            RuleFor(x => x.Code).NotEmpty().WithMessage("Code is required.").MinimumLength(3).WithMessage("Code must be at least 3 characters long.").MaximumLength(6).WithMessage("Code cant be longer than 6 characters").Must(code => !context.Coupons.Any(c=> c.Code == code)).WithMessage("Code already exists.");
            RuleFor(x => x.DiscountAmount).GreaterThan(0).WithMessage("Discount amount must be a positive value.").LessThan(50).WithMessage("Discount amount must be less than 50.");
            RuleFor(x => x.ExpiryDate).GreaterThan(DateTime.Now).WithMessage("Expiry date must be in the future.");
        }
    }
}
