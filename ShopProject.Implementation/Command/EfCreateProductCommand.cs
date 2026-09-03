using ShopProject.Application.Command;
using ShopProject.Application.DataTransfer;
using ShopProject.DataAccess;
using ShopProject.Implementation.Validators;
using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
namespace ShopProject.Implementation.Command
{
    public class EfCreateProductCommand : ICreateProductCommand
    {
        private readonly ShopProjectContext _context;
        private readonly CreateProductValidator _validator;

        public EfCreateProductCommand(ShopProjectContext context, CreateProductValidator validator)
        {
            _context = context;
            _validator = validator;
        }

        public int Id => 8;

        public string Name => "Create new product";

        public void Execute(ProductDto request)
        {
            _validator.ValidateAndThrow(request);
            var product = new Domain.Product
            {
                Name = request.Name,
                Description = request.Description,
                Price = request.Price,
                CategoryId = request.CategoryId
            };
            _context.Products.Add(product);
            _context.SaveChanges();

        }
    }
}
