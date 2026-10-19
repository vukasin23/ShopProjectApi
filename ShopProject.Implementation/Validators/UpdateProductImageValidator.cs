using FluentValidation;
using ShopProject.Application.DataTransfer;
using ShopProject.DataAccess;

namespace ShopProject.Implementation.Validators;

public class UpdateProductImageValidator : AbstractValidator<UpdateProductImageDto>
{
    public UpdateProductImageValidator(ShopProjectContext context)
    {
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

        RuleFor(x => x.IsPrimary).NotNull().WithMessage("IsPrimary is required.");

        // Another image of the same product may not be primary, the image being updated may already be.
        RuleFor(x => x)
            .Must(x => x.IsPrimary != true || !context.ProductImages.Any(i =>
                i.Id != x.Id &&
                i.IsPrimary &&
                i.ProductId == context.ProductImages.Where(pi => pi.Id == x.Id).Select(pi => pi.ProductId).FirstOrDefault()))
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
