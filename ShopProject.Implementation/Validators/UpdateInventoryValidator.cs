using FluentValidation;
using ShopProject.Application.DataTransfer;

namespace ShopProject.Implementation.Validators;

public class UpdateInventoryValidator : AbstractValidator<UpdateInventoryDto>
{
    public UpdateInventoryValidator()
    {
        RuleFor(x => x.Quantity)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Quantity ne može biti negativan.");
    }
}
