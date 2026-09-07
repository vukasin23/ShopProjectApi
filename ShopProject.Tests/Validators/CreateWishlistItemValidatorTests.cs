using ShopProject.Application.DataTransfer;
using ShopProject.DataAccess;
using ShopProject.Implementation.Validators;
using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation.TestHelper;
namespace ShopProject.Tests.Validators
{
    public class CreateWishlistItemValidatorTests
    {
        private static WishlistitemDto CreateDto()
        {
            return new WishlistitemDto
            {
                UserId = 999999,
                ProductId = 999999,
                AddedAt = DateTime.Now
            };
        }

        [Fact]
        public void Validate_WhenUserDoesNotExist_ShouldHaveValidationError()
        {
            var context = new ShopProjectContext();
            var validator = new CreateWishlistItemValidator(context);

            var dto = CreateDto();

            var result = validator.TestValidate(dto);

            result.ShouldHaveValidationErrorFor(x => x.UserId);
        }

        [Fact]
        public void Validate_WhenProductDoesNotExist_ShouldHaveValidationError()
        {
            var context = new ShopProjectContext();
            var validator = new CreateWishlistItemValidator(context);

            var dto = CreateDto();

            var result = validator.TestValidate(dto);

            result.ShouldHaveValidationErrorFor(x => x.ProductId);
        }

        [Fact]
        public void Validate_WhenAddedAtIsInFuture_ShouldHaveValidationError()
        {
            var context = new ShopProjectContext();
            var validator = new CreateWishlistItemValidator(context);

            var dto = CreateDto();
            dto.AddedAt = DateTime.Now.AddMinutes(10);

            var result = validator.TestValidate(dto);

            result.ShouldHaveValidationErrorFor(x => x.AddedAt);
        }

        [Fact]
        public void Validate_WhenWishlistItemAlreadyExists_ShouldHaveValidationError()
        {
            var context = new ShopProjectContext();
            var existingWishlistItem = context.WishlistItems.FirstOrDefault();

            Assert.NotNull(existingWishlistItem);

            var validator = new CreateWishlistItemValidator(context);

            var dto = new WishlistitemDto
            {
                UserId = existingWishlistItem!.UserId,
                ProductId = existingWishlistItem.ProductId,
                AddedAt = DateTime.Now
            };

            var result = validator.TestValidate(dto);

            Assert.False(result.IsValid);
        }
    }
}
