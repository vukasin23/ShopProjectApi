using FluentValidation;
using ShopProject.Application.DataTransfer;
using ShopProject.DataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopProject.Implementation.Validators
{
    public class CreateProductImageValidator : AbstractValidator<ProductImageDto>
    {
        public CreateProductImageValidator(ShopProjectContext context)
        {
            RuleFor(x => x.ProductId)
                .NotEmpty()
                .WithMessage("Product is required.")
                .Must(id => context.Products.Any(p => p.Id == id))
                .WithMessage("Selected product does not exist.");

            RuleFor(x => x.ImageUrl)
                .NotEmpty()
                .WithMessage("Image URL is required.")
                .MaximumLength(500)
                .WithMessage("Image URL can't be longer than 500 characters.")
                .Must(HaveValidImageExtension)
                .WithMessage("Image must be jpg, jpeg, png, webp or gif.");

            RuleFor(x => x.AltText)
                .NotEmpty()
                .WithMessage("Alt text is required.")
                .MinimumLength(3)
                .WithMessage("Alt text must be at least 3 characters long.")
                .MaximumLength(150)
                .WithMessage("Alt text can't be longer than 150 characters.");

            RuleFor(x => x)
                .Must(x => !x.IsPrimary || !context.ProductImages.Any(i => i.ProductId == x.ProductId && i.IsPrimary))
                .WithMessage("This product already has a primary image.");
        }

        private static bool HaveValidImageExtension(string url)
        {
            var lowerUrl = url.ToLower();

            return lowerUrl.EndsWith(".jpg")
                   || lowerUrl.EndsWith(".jpeg")
                   || lowerUrl.EndsWith(".png")
                   || lowerUrl.EndsWith(".webp")
                   || lowerUrl.EndsWith(".gif");
        }
    }
}
