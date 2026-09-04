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
    public class EfCreateProductSpecificationCommand : ICreateProductSpecificationCommand
    {
        private readonly ShopProjectContext _context;
        private readonly CreateProductSpecificationValidator _validator;

        public EfCreateProductSpecificationCommand(ShopProjectContext context, CreateProductSpecificationValidator validator)
        {
            _context = context;
            _validator = validator;
        }

        public int Id => 10;

        public string Name => "Create product specification";

        public void Execute(ProductSpecificationDto request)
        {
            _validator.ValidateAndThrow(request);
            var ProductSpecification = new ProductSpecification
            {
                ProductId = request.ProductId,
                SpecificationName = request.SpecificationName,
                SpecificationValue = request.SpecificationValue
            };

            _context.ProductSpecifications.Add(ProductSpecification);
            _context.SaveChanges();
        }
    }
}
