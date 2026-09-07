using FluentValidation.TestHelper;
using ShopProject.Application.DataTransfer;
using ShopProject.DataAccess;
using ShopProject.Implementation.Validators;
using System;
using System.Linq;
using Xunit;

namespace ShopProject.Tests.Validators
{
    public class CreateCartValidatorTests
    {
        private static CartDto CreateDto()
        {
            return new CartDto
            {
                UserId = 999999,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };
        }

        [Fact]
        public void Validate_WhenUserDoesNotExist_ShouldHaveValidationError()
        {
            var context = new ShopProjectContext();
            var validator = new CreateCartValidator(context);

            var dto = CreateDto();

            var result = validator.TestValidate(dto);

            result.ShouldHaveValidationErrorFor(x => x.UserId);
        }

        [Fact]
        public void Validate_WhenCreatedAtIsEmpty_ShouldHaveValidationError()
        {
            var context = new ShopProjectContext();
            var validator = new CreateCartValidator(context);

            var dto = CreateDto();
            dto.CreatedAt = default;

            var result = validator.TestValidate(dto);

            result.ShouldHaveValidationErrorFor(x => x.CreatedAt);
        }

        [Fact]
        public void Validate_WhenCreatedAtIsInFuture_ShouldHaveValidationError()
        {
            var context = new ShopProjectContext();
            var validator = new CreateCartValidator(context);

            var dto = CreateDto();
            dto.CreatedAt = DateTime.Now.AddMinutes(10);

            var result = validator.TestValidate(dto);

            result.ShouldHaveValidationErrorFor(x => x.CreatedAt);
        }

        [Fact]
        public void Validate_WhenUpdatedAtIsBeforeCreatedAt_ShouldHaveValidationError()
        {
            var context = new ShopProjectContext();
            var validator = new CreateCartValidator(context);

            var dto = CreateDto();
            dto.CreatedAt = DateTime.Now;
            dto.UpdatedAt = dto.CreatedAt.AddMinutes(-1);

            var result = validator.TestValidate(dto);

            result.ShouldHaveValidationErrorFor(x => x.UpdatedAt);
        }

        [Fact]
        public void Validate_WhenUserAlreadyHasCart_ShouldHaveValidationError()
        {
            var context = new ShopProjectContext();
            var existingCart = context.Carts.FirstOrDefault();

            Assert.NotNull(existingCart);

            var validator = new CreateCartValidator(context);

            var dto = new CartDto
            {
                UserId = existingCart!.UserId,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };

            var result = validator.TestValidate(dto);

            Assert.False(result.IsValid);
        }
    }
}