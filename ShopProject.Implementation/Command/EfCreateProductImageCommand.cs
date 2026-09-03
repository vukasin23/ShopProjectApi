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
    public class EfCreateProductImageCommand : ICreateProductImageCommand
    {
        private readonly ShopProjectContext _context;
        private readonly CreateProductImageValidator _validator;

        public EfCreateProductImageCommand(ShopProjectContext context, CreateProductImageValidator validator)
        {
            _context = context;
            _validator = validator;
        }

        public int Id => 9;

        public string Name => "Create new product image";

        public void Execute(ProductImageDto request)
        {
            _validator.ValidateAndThrow(request);
            var productImage = new Domain.ProductImage
            {
                ProductId = request.ProductId,
                ImageUrl = request.ImageUrl,
                AltText = request.AltText,
                IsPrimary = request.IsPrimary
            };

            _context.ProductImages.Add(productImage);
            _context.SaveChanges();
        }
    }
}
