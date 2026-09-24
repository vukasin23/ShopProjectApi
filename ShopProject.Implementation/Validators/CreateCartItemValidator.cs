using FluentValidation;
using ShopProject.Application;
using ShopProject.Application.DataTransfer;
using ShopProject.DataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopProject.Implementation.Validators
{
    public class CreateCartItemValidator : AbstractValidator<CartItemDto>
    {
        private readonly ShopProjectContext _context;
        private readonly IApplicationActor _actor;

        public CreateCartItemValidator(ShopProjectContext context, IApplicationActor actor)
        {
            _context = context;
            _actor = actor;
            // The item always goes into the actor's own cart, so that cart must exist.
            RuleFor(x => x)
                .Must(_ => _context.Carts.Any(c => c.UserId == _actor.Id))
                .WithMessage("User does not have a cart.");

            RuleFor(x => x.ProductId)
                .GreaterThan(0)
                .Must(productId => _context.Products.Any(p => p.Id == productId))
                .WithMessage("Product does not exist.");

            RuleFor(x => x.Quantity)
                .GreaterThan(0)
                .WithMessage("Quantity must be greater than 0.");

            RuleFor(x => x)
                .Must(dto => !_context.CartItems.Any(ci =>
                    ci.Cart.UserId == _actor.Id &&
                    ci.ProductId == dto.ProductId))
                .WithMessage("This product is already in the cart.")
                .When(x => x.ProductId > 0);
        }
    }
}
