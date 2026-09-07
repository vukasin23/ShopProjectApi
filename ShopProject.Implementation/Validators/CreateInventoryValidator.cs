using FluentValidation;
using ShopProject.Application.DataTransfer;
using ShopProject.DataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopProject.Implementation.Validators
{
    public class CreateInventoryValidator:AbstractValidator<InventoryDto>
    {
        private readonly ShopProjectContext _context;

        public CreateInventoryValidator(ShopProjectContext context)
        {
            _context = context;

            RuleFor(x => x.Quantity)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Quantity ne može biti negativan.");

            RuleFor(x => x.StoreId)
                .GreaterThan(0)
                .Must(StoreExists)
                .WithMessage("Store ne postoji.");

            RuleFor(x => x.ProductId)
                .GreaterThan(0)
                .Must(ProductExists)
                .WithMessage("Product ne postoji.");

            RuleFor(x => x.ProductId)
                .Must((dto, productId) =>
                    !_context.Inventories.Any(x =>
                        x.ProductId == productId &&
                        x.StoreId == dto.StoreId))
                .WithMessage("Za ovaj proizvod u toj prodavnici inventar već postoji.")
                .When(x => x.ProductId > 0 && x.StoreId > 0);

        }

        private bool StoreExists(int storeId)
        {
            return _context.Stores.Any(x => x.Id == storeId);
        }

        private bool ProductExists(int productId)
        {
            return _context.Products.Any(x => x.Id == productId);
        }
    }
}
