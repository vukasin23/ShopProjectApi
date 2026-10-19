using FluentValidation;
using ShopProject.Application.DataTransfer;
using ShopProject.DataAccess;

namespace ShopProject.Implementation.Validators;

public class UpdateProductSpecificationValidator : AbstractValidator<UpdateProductSpecificationDto>
{
    public UpdateProductSpecificationValidator(ShopProjectContext context)
    {
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

        // The name must stay unique among the other specifications of the same product.
        RuleFor(x => x)
            .Must(dto => !context.ProductSpecifications.Any(ps =>
                ps.Id != dto.Id &&
                ps.ProductId == context.ProductSpecifications.Where(s => s.Id == dto.Id).Select(s => s.ProductId).FirstOrDefault() &&
                ps.SpecificationName.ToLower() == dto.SpecificationName.ToLower()))
            .WithMessage("Specifikacija sa tim nazivom već postoji za ovaj proizvod.")
            .When(x => !string.IsNullOrEmpty(x.SpecificationName));
    }
}
