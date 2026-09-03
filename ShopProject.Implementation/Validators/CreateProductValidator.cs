using FluentValidation;
using ShopProject.Application.DataTransfer;
using ShopProject.DataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopProject.Implementation.Validators
{
    public class CreateProductValidator:AbstractValidator<ProductDto>
    {
        public CreateProductValidator(ShopProjectContext context)
        {
            RuleFor(x=>x.Name).NotEmpty().WithMessage("Product name is required.")
                .MinimumLength(3).WithMessage("Product name must be at least 3 characters long.")
                .MaximumLength(30).WithMessage("Product name must be at most 30 characters long.")
                .Must(name => !context.Products.Any(p => p.Name == name)).WithMessage("Product with that name already exists.");
            RuleFor(x=>x.Description).NotEmpty().WithMessage("Product description is required.")
                .MinimumLength(10).WithMessage("Product description must be at least 10 characters long.")
                .MaximumLength(200).WithMessage("Product description must be at most 200 characters long.");
            RuleFor(x=>x.Price).NotEmpty().WithMessage("Product price is required.")
                .GreaterThan(0).WithMessage("Product price must be a positive number.");
            RuleFor(x => x.CategoryId).NotEmpty().WithMessage("Product category is required.")
                .Must(id => context.Categories.Any(c => c.Id == id)).WithMessage("Selected category does not exist.");
        }
    }
}
