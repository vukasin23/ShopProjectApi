using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
using ShopProject.Application.DataTransfer;
using ShopProject.DataAccess;

namespace ShopProject.Implementation.Validators
{
    public class CreateAddressValidator:AbstractValidator<AddressDto>
    {

        public CreateAddressValidator(ShopProjectContext context)
        {
            RuleFor(x => x.UserId).NotEmpty().WithMessage("UserId is required.").Must(userId => context.Users.Any(u => u.Id == userId)).Must(userId => !context.Addresses.Any(a=>a.UserId == userId));
            RuleFor(x => x.Street).NotEmpty().WithMessage("Street is required.").MinimumLength(3).WithMessage("Street must be at least 3 characters long.").MaximumLength(100).WithMessage("Street must not exceed 100 characters.");
            RuleFor(x => x.City).NotEmpty().WithMessage("City is required.").MinimumLength(2).WithMessage("City must be at least 2 characters long.").MaximumLength(100).WithMessage("City must not exceed 100 characters.");
            RuleFor(x => x.ZipCode).NotEmpty().WithMessage("ZipCode is required.").Matches(@"^\d{5}(-\d{4})?$").WithMessage("Invalid zip code format.");
            RuleFor(x => x.Country).NotEmpty().WithMessage("Country is required.").MinimumLength(2).WithMessage("Country must be at least 2 characters long.").MaximumLength(100).WithMessage("Country must not exceed 100 characters.");
        }
    }
}
