using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using FluentValidation;
using ShopProject.Application.DataTransfer;
using ShopProject.DataAccess;
namespace ShopProject.Implementation.Validators
{
    public class CreateStoreValidator:AbstractValidator<StoreDto>
    {
        public CreateStoreValidator(ShopProjectContext context):base()
        {
            RuleFor(x=>x.Name).NotEmpty().WithMessage("Store must have a name")
                .MinimumLength(3).WithMessage("Store name must be at least 3 characters long")
                .MaximumLength(20).WithMessage("Store name must be at most 20 characters long")
                .Must(name=> !context.Stores.Any(s=>s.Name == name)).WithMessage("Store with this name already exists");
            
            RuleFor(x=>x.Description).NotEmpty().MaximumLength(100).WithMessage("Store description must be at most 100 characters long");
            RuleFor(x=>x.City).NotEmpty().WithMessage("Store must have a city")
                .MinimumLength(3).WithMessage("Store city must be at least 3 characters long")
                .MaximumLength(20).WithMessage("Store city must be at most 20 characters long");
            RuleFor(x => x.Address).NotEmpty().WithMessage("Store must have an address")
                .MinimumLength(5).WithMessage("Store address must be at least 5 characters long")
                .MaximumLength(100).WithMessage("Store address must be at most 100 characters long");
            RuleFor(x=>x.Phone).NotEmpty().WithMessage("Store must have a phone number")
                .Matches(@"^06\d{7,8}$")
                .WithMessage("Phone number must be Serbian mobile number, example: 0612345678.");
        }
    }
}
