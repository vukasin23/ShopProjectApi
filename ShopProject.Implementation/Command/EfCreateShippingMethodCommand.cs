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
    public class EfCreateShippingMethodCommand : ICreateShippingMethods
    {
        private readonly ShopProjectContext _context;
        private readonly CreateShippingMethodValidator _validator;

        public EfCreateShippingMethodCommand(ShopProjectContext context, CreateShippingMethodValidator validator)
        {
            _context = context;
            _validator = validator;
        }

        public int Id => 6;

        public string Name => "Create new shipping method";

        public void Execute(ShippingMethodDto request)
        {
            _validator.ValidateAndThrow(request);

            var shippingMethod = new ShippingMethod
            {
                Name = request.Name,
                Price = request.Price,
                Description = request.Description
            };

            _context.ShippingMethods.Add(shippingMethod);
            _context.SaveChanges();

        }
    }
}
