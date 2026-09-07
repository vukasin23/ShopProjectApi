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
    public class EfCreateCartCommand : ICreateCartCommand
    {
        private readonly ShopProjectContext _context;
        private readonly CreateCartValidator _validator;

        public EfCreateCartCommand(ShopProjectContext context, CreateCartValidator validator)
        {
            _context = context;
            _validator = validator;
        }

        public int Id => 13;

        public string Name => "Create cart command";

        public void Execute(CartDto request)
        {
            _validator.ValidateAndThrow(request);

            var Cart = new Domain.Cart
            {
                UserId = request.UserId,
                CreatedAt = request.CreatedAt,
                UpdatedAt = request.UpdatedAt
            };
            _context.Carts.Add(Cart);
            _context.SaveChanges();
        }
    }
}
