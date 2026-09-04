using FluentValidation;
using ShopProject.Application.DataTransfer;
using ShopProject.DataAccess;
using System;
using System.Collections.Generic;
using System.Text;
namespace ShopProject.Implementation.Validators
{
    public class CreateProductSpecificationValidator:AbstractValidator<ProductSpecificationDto>
    {
        public CreateProductSpecificationValidator(ShopProjectContext context)
        {
            RuleFor(x => x.ProductId)
                .GreaterThan(0)
                .WithMessage("ProductId mora biti validan.")
                .Must(productId => context.Products.Any(p => p.Id == productId))
                .WithMessage("Proizvod ne postoji.");

            RuleFor(x => x.SpecificationName)
                .NotEmpty()
                .WithMessage("Naziv specifikacije je obavezan.")
                .MaximumLength(100)
                .WithMessage("Naziv specifikacije može imati najviše 100 karaktera.");

            RuleFor(x => x.SpecificationValue)
                .NotEmpty()
                .WithMessage("Vrednost specifikacije je obavezna.")
                .MaximumLength(500)
                .WithMessage("Vrednost specifikacije može imati najviše 500 karaktera.");

            RuleFor(x => x)
                .Must(dto => !context.ProductSpecifications.Any(ps =>
                    ps.ProductId == dto.ProductId &&
                    ps.SpecificationName.ToLower() == dto.SpecificationName.ToLower()))
                .WithMessage("Specifikacija sa tim nazivom već postoji za ovaj proizvod.");
        }
    }
}
