using FluentValidation.TestHelper;
using ShopProject.Application.DataTransfer;
using ShopProject.DataAccess;
using ShopProject.Implementation;
using ShopProject.Implementation.Validators;
using System;
using System.Linq;
using Xunit;

namespace ShopProject.Tests.Validators
{
    public class CreateCartItemValidatorTests
    {
        private static CreateCartItemValidator CreateValidator(ShopProjectContext context, int actorId)
        {
            return new CreateCartItemValidator(context, new Actor { Id = actorId });
        }

        // A valid request: the actor owns an existing cart and the product is not in that cart yet.
        private static (CartItemDto dto, int actorId, int cartId) CreateValidDto(ShopProjectContext context)
        {
            var cart = context.Carts.FirstOrDefault();
            Assert.NotNull(cart);

            var product = context.Products.FirstOrDefault(p =>
                !context.CartItems.Any(ci => ci.CartId == cart!.Id && ci.ProductId == p.Id));
            Assert.NotNull(product);

            var dto = new CartItemDto
            {
                ProductId = product!.Id,
                Quantity = 1
            };

            return (dto, cart!.UserId, cart.Id);
        }

        [Fact]
        public void Validate_WhenRequestIsValid_ShouldNotHaveValidationErrors()
        {
            var context = new ShopProjectContext();
            var (dto, actorId, _) = CreateValidDto(context);
            var validator = CreateValidator(context, actorId);

            var result = validator.TestValidate(dto);

            Assert.True(result.IsValid);
        }

        [Fact]
        public void Validate_WhenActorDoesNotHaveCart_ShouldHaveValidationError()
        {
            var context = new ShopProjectContext();
            var (dto, _, _) = CreateValidDto(context);
            var validator = CreateValidator(context, 999999);

            var result = validator.TestValidate(dto);

            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.ErrorMessage == "User does not have a cart.");
        }

        [Fact]
        public void Validate_WhenActorIsUnauthorized_ShouldHaveValidationError()
        {
            var context = new ShopProjectContext();
            var (dto, _, _) = CreateValidDto(context);
            var validator = new CreateCartItemValidator(context, new UnauthorizedActor());

            var result = validator.TestValidate(dto);

            Assert.False(result.IsValid);
        }

        [Fact]
        public void Validate_WhenProductIdIsZero_ShouldHaveValidationError()
        {
            var context = new ShopProjectContext();
            var (dto, actorId, _) = CreateValidDto(context);
            dto.ProductId = 0;
            var validator = CreateValidator(context, actorId);

            var result = validator.TestValidate(dto);

            result.ShouldHaveValidationErrorFor(x => x.ProductId);
        }

        [Fact]
        public void Validate_WhenProductDoesNotExist_ShouldHaveValidationError()
        {
            var context = new ShopProjectContext();
            var (dto, actorId, _) = CreateValidDto(context);
            dto.ProductId = 999999;
            var validator = CreateValidator(context, actorId);

            var result = validator.TestValidate(dto);

            result.ShouldHaveValidationErrorFor(x => x.ProductId)
                .WithErrorMessage("Product does not exist.");
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void Validate_WhenQuantityIsNotPositive_ShouldHaveValidationError(int quantity)
        {
            var context = new ShopProjectContext();
            var (dto, actorId, _) = CreateValidDto(context);
            dto.Quantity = quantity;
            var validator = CreateValidator(context, actorId);

            var result = validator.TestValidate(dto);

            result.ShouldHaveValidationErrorFor(x => x.Quantity);
        }

        [Fact]
        public void Validate_WhenProductIsAlreadyInCart_ShouldHaveValidationError()
        {
            var context = new ShopProjectContext();
            var (dto, actorId, cartId) = CreateValidDto(context);

            // Insert the item inside a transaction that is rolled back on dispose,
            // so the test does not depend on existing data or leave any behind.
            using var transaction = context.Database.BeginTransaction();
            context.CartItems.Add(new Domain.CartItem
            {
                CartId = cartId,
                ProductId = dto.ProductId,
                Quantity = 1,
                AddedAt = DateTime.Now
            });
            context.SaveChanges();

            var validator = CreateValidator(context, actorId);

            var result = validator.TestValidate(dto);

            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.ErrorMessage == "This product is already in the cart.");
        }
    }
}
