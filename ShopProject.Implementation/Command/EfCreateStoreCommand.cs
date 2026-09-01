using ShopProject.Application.Command;
using ShopProject.Application.DataTransfer;
using ShopProject.DataAccess;
using ShopProject.Domain;
using ShopProject.Implementation.Validators;
using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;

namespace ShopProject.Implementation.Command
{
    public class EfCreateStoreCommand:ICreateStoreCommand
    {
        private readonly ShopProjectContext _context;
        private readonly CreateStoreValidator _validator;  

        public EfCreateStoreCommand(ShopProjectContext context, CreateStoreValidator validator)
        {
            _context = context;
            _validator = validator;
        }
        public int Id => 4;
        public string Name => "Create Store using EF";
        public void Execute(StoreDto request)
        {
            _validator.ValidateAndThrow(request);
            var store = new Store
            {
                Name = request.Name,
                Description = request.Description,
                City = request.City,
                Address = request.Address,
                Phone = request.Phone,
                IsActive = request.IsActive
            };
            _context.Stores.Add(store);
            _context.SaveChanges();
        }
    }
}
