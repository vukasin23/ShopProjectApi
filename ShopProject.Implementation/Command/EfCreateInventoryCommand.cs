using FluentValidation;
using ShopProject.Application.Command;
using ShopProject.Application.DataTransfer;
using ShopProject.DataAccess;
using ShopProject.Implementation.Validators;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopProject.Implementation.Command
{
    public class EfCreateInventoryCommand : ICreateInventoryCommand
    {
        private readonly ShopProjectContext _context;
        private readonly CreateInventoryValidator _validator;

        public EfCreateInventoryCommand(ShopProjectContext context, CreateInventoryValidator validator)
        {
            _context = context;
            _validator = validator;
        }

        public int Id => 11;

        public string Name => "Create Inventory";

        public void Execute(InventoryDto request)
        {
            _validator.ValidateAndThrow(request);

            var inventory = new Domain.Inventory
            {
                Quantity = request.Quantity,
                StoreId = request.StoreId,
                LastUpdated = request.LastUpdated,
                ProductId = request.ProductId
            };

            _context.Inventories.Add(inventory);
            _context.SaveChanges();
        }
    }
}
