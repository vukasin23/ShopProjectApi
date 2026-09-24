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
    public class EfCreateCartItemCommand:ICreateCartItemCommand
    {
        private readonly ShopProjectContext _context;
        private readonly IApplicationActor _actor;

        private readonly CreateCartItemValidator _validator;
        public EfCreateCartItemCommand(ShopProjectContext context, IApplicationActor actor, CreateCartItemValidator validator)
        {
            _context = context;
            _actor = actor;
            _validator = validator;
        }

        public int Id => 14;
        public string Name => "Create Cart Item using EF";

        public void Execute(CartItemDto request)
        {
            _validator.ValidateAndThrow(request);
            var cart = _context.Carts.FirstOrDefault(c => c.UserId == _actor.Id);
            var cartItem = new Domain.CartItem
            {
                CartId = cart.Id,
                ProductId = request.ProductId,
                Quantity = request.Quantity,
                AddedAt = DateTime.Now
            };

            _context.CartItems.Add(cartItem);
            _context.SaveChanges();
        }
    }
}
