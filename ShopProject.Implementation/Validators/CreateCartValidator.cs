using FluentValidation;
using ShopProject.Application;
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
        private readonly IApplicationActor _actor;

        public CreateCartValidator(ShopProjectContext context, IApplicationActor actor)
        {
            _context = context;
            _actor = actor;

            RuleFor(x => x)
                .Must(_ => _context.Users.Any(u => u.Id == _actor.Id))
                .WithMessage("User does not exist.");

            RuleFor(x => x)
                .Must(_ => !_context.Carts.Any(c => c.UserId == _actor.Id))
                .WithMessage("This user already has a cart.")
                .When(_ => _context.Users.Any(u => u.Id == _actor.Id));
        }
    }
}
