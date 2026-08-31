using FluentValidation;
using ShopProject.Application.DataTransfer;
using ShopProject.DataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopProject.Implementation.Validators
{
    public class CreateCategoryValidator:AbstractValidator<CategoryDto>
    {
        public CreateCategoryValidator(ShopProjectContext context):base()
        {
            RuleFor(x => x.Name).NotEmpty().MinimumLength(3)
                .WithMessage("Name must be at least 3 characters long.")
                .MaximumLength(20)
                .WithMessage("Name must be at most 20 characters long.")
                .Must(name => !context.Categories.Any(c => c.Name == name))
                .WithMessage("Category with this name already exists.");
            RuleFor(x => x.Description)
                .MaximumLength(500)
                .WithMessage("Description must have maximum 500 characters.");

        }
    }
}
