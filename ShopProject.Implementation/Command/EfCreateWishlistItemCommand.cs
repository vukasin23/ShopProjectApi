using FluentValidation;
using ShopProject.Application.Command;
using ShopProject.Application.DataTransfer;
using ShopProject.DataAccess;
using ShopProject.Domain;
using ShopProject.Implementation.Validators;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopProject.Implementation.Command
{
    public class EfCreateWishlistItemCommand : ICreateWishlistItemCommand
    {
        private readonly ShopProjectContext context;
        private readonly CreateWishlistItemValidator validator;

        public EfCreateWishlistItemCommand(ShopProjectContext context, CreateWishlistItemValidator validator)
        {
            this.context = context;
            this.validator = validator;
        }

        public int Id => 12;

        public string Name => "Create Wishlist Item";

        public void Execute(WishlistitemDto request)
        {
            validator.ValidateAndThrow(request);

            var wishlistItem = new WishlistItem
            {
                UserId = request.UserId,
                ProductId = request.ProductId,
                AddedAt = request.AddedAt
            };  

            context.WishlistItems.Add(wishlistItem);
            context.SaveChanges();
        }
    }
}
