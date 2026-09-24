using FluentValidation;
using ShopProject.Application;
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
        private readonly IApplicationActor _actor;

        public EfCreateCartCommand(ShopProjectContext context, CreateCartValidator validator, IApplicationActor actor)
        {
            _context = context;
            _validator = validator;
            _actor = actor;
        }

        public int Id => 13;

        public string Name => "Create cart command";

        public void Execute(CartDto request)
        {
            _validator.ValidateAndThrow(request);

            var now = DateTime.Now;

            var cart = new Domain.Cart
            {
                UserId = _actor.Id,
                CreatedAt = now,
                UpdatedAt = now
            };
            _context.Carts.Add(cart);
            _context.SaveChanges();
        }
    }
}
