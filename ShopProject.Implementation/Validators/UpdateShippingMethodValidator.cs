using FluentValidation;
using ShopProject.Application.DataTransfer;
using ShopProject.DataAccess;

namespace ShopProject.Implementation.Validators;

public class UpdateShippingMethodValidator : AbstractValidator<UpdateShippingMethodDto>
{
    public UpdateShippingMethodValidator(ShopProjectContext context)
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required.")
            .Must((dto, name) => !context.ShippingMethods.Any(s => s.Name == name && s.Id != dto.Id))
            .WithMessage("Shipping method with that name already exists.");
        RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price must be a positive value.");
        RuleFor(x => x.Description).NotEmpty().WithMessage("Description is required.");
    }
}
