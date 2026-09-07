using FluentValidation;
using ShopProject.Application.DataTransfer;
using ShopProject.DataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopProject.Implementation.Validators
{
    public class CreateCartValidator : AbstractValidator<CartDto>
    {
        private readonly ShopProjectContext _context;

        public CreateCartValidator(ShopProjectContext context)
        {
            _context = context;

            RuleFor(x => x.UserId)
                .GreaterThan(0)
                .Must(userId => _context.Users.Any(x => x.Id == userId))
                .WithMessage("User does not exist.");

            RuleFor(x => x)
                .Must(dto => !_context.Carts.Any(x => x.UserId == dto.UserId))
                .WithMessage("This user already has a cart.")
                .When(x => x.UserId > 0);

            RuleFor(x => x.CreatedAt)
                .NotEmpty()
                .LessThanOrEqualTo(DateTime.Now)
                .WithMessage("CreatedAt must be a valid date.");

            RuleFor(x => x.UpdatedAt)
                .NotEmpty()
                .GreaterThanOrEqualTo(x => x.CreatedAt)
                .WithMessage("UpdatedAt must be greater than or equal to CreatedAt.");
        }
    }
}