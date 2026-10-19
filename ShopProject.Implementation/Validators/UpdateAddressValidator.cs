using FluentValidation;
using ShopProject.Application.DataTransfer;

namespace ShopProject.Implementation.Validators;

public class UpdateAddressValidator : AbstractValidator<UpdateAddressDto>
{
    public UpdateAddressValidator()
    {
        RuleFor(x => x.Street).NotEmpty().WithMessage("Street is required.").MinimumLength(3).WithMessage("Street must be at least 3 characters long.").MaximumLength(100).WithMessage("Street must not exceed 100 characters.");
        RuleFor(x => x.City).NotEmpty().WithMessage("City is required.").MinimumLength(2).WithMessage("City must be at least 2 characters long.").MaximumLength(100).WithMessage("City must not exceed 100 characters.");
        RuleFor(x => x.ZipCode).NotEmpty().WithMessage("ZipCode is required.").Matches(@"^\d{5}(-\d{4})?$").WithMessage("Invalid zip code format.");
        RuleFor(x => x.Country).NotEmpty().WithMessage("Country is required.").MinimumLength(2).WithMessage("Country must be at least 2 characters long.").MaximumLength(100).WithMessage("Country must not exceed 100 characters.");
    }
}
