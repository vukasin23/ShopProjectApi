using FluentValidation;
using ShopProject.Application.DataTransfer;
using ShopProject.DataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopProject.Implementation.Validators
{
    public class CreateShippingMethodValidator:AbstractValidator<ShippingMethodDto>
    {

        public CreateShippingMethodValidator(ShopProjectContext context)
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required.").Must(name=>!context.ShippingMethods.Any(s=>s.Name == name)).WithMessage("Shipping method with that name already exists.");
            RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price must be a positive value.");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Description is required.");
        }
    }
}
