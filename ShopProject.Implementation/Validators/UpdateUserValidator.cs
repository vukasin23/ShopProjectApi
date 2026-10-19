using FluentValidation;
using ShopProject.Application.DataTransfer;
using ShopProject.DataAccess;

namespace ShopProject.Implementation.Validators;

public class UpdateUserValidator : AbstractValidator<UpdateUserDto>
{
    public UpdateUserValidator(ShopProjectContext context)
    {
        RuleFor(x => x.FirstName).NotEmpty().WithMessage("First name is required.")
            .MinimumLength(2).WithMessage("First name must be at least 2 characters long.")
            .MaximumLength(30).WithMessage("First name must be at most 30 characters long.");
        RuleFor(x => x.LastName).NotEmpty().WithMessage("Last name is required.")
            .MinimumLength(2).WithMessage("Last name must be at least 2 characters long.")
            .MaximumLength(30).WithMessage("Last name must be at most 30 characters long.");

        // Deleted users are included, their username and email stay taken.
        RuleFor(x => x.Username).NotEmpty().WithMessage("Username is required.")
            .MinimumLength(3).WithMessage("Username must be at least 3 characters long.")
            .MaximumLength(20).WithMessage("Username must be at most 20 characters long.")
            .Must((dto, username) => !context.Users.Any(u => u.Username == username && u.Id != dto.Id))
            .WithMessage("Username is already taken.");
        RuleFor(x => x.Email).NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Email is not valid.")
            .Must((dto, email) => !context.Users.Any(u => u.Email == email && u.Id != dto.Id))
            .WithMessage("Email is already taken.");
        RuleFor(x => x.PhoneNumber).NotEmpty().WithMessage("Phone number is required.")
            .Matches(@"^06\d{7,8}$")
            .WithMessage("Phone number must be Serbian mobile number, example: 0612345678.");
    }
}
