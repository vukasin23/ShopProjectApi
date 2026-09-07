using FluentValidation;
using ShopProject.Application.DataTransfer;
using ShopProject.DataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopProject.Implementation.Validators
{
    public class CreateWishlistItemValidator : AbstractValidator<WishlistitemDto>
    {
        private readonly ShopProjectContext _context;

        public CreateWishlistItemValidator(ShopProjectContext context)
        {
            _context = context;

            RuleFor(x => x.UserId)
                .GreaterThan(0)
                .Must(userId => _context.Users.Any(x => x.Id == userId))
                .WithMessage("User ne postoji.");

            RuleFor(x => x.ProductId)
                .GreaterThan(0)
                .Must(productId => _context.Products.Any(x => x.Id == productId))
                .WithMessage("Product ne postoji.");

            RuleFor(x => x)
                .Must(dto => !_context.WishlistItems.Any(x =>
                    x.UserId == dto.UserId &&
                    x.ProductId == dto.ProductId))
                .WithMessage("Proizvod se već nalazi na wishlisti.")
                .When(x => x.UserId > 0 && x.ProductId > 0);

            RuleFor(x => x.AddedAt)
                .LessThanOrEqualTo(DateTime.Now)
                .WithMessage("AddedAt ne može biti u budućnosti.");
        }
    }
}
