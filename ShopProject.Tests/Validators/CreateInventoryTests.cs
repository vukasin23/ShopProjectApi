using FluentValidation.TestHelper;
using ShopProject.Application.DataTransfer;
using ShopProject.DataAccess;
using ShopProject.Implementation.Validators;
using System;
using System.Linq;
using Xunit;

namespace ShopProject.Tests.Validators
{
    public class CreateInventoryValidatorTests
    {
        private static InventoryDto CreateDto()
        {
            return new InventoryDto
            {
                Quantity = 10,
                StoreId = 999999,
                ProductId = 999999,
                LastUpdated = DateTime.Now
            };
        }

        [Fact]
        public void Validate_WhenProductDoesNotExist_ShouldHaveValidationError()
        {
            var context = new ShopProjectContext();
            var validator = new CreateInventoryValidator(context);

            var dto = CreateDto();

            var result = validator.TestValidate(dto);

            result.ShouldHaveValidationErrorFor(x => x.ProductId);
        }

        [Fact]
        public void Validate_WhenStoreDoesNotExist_ShouldHaveValidationError()
        {
            var context = new ShopProjectContext();
            var validator = new CreateInventoryValidator(context);

            var dto = CreateDto();

            var result = validator.TestValidate(dto);

            result.ShouldHaveValidationErrorFor(x => x.StoreId);
        }

        [Fact]
        public void Validate_WhenQuantityIsNegative_ShouldHaveValidationError()
        {
            var context = new ShopProjectContext();
            var validator = new CreateInventoryValidator(context);

            var dto = CreateDto();
            dto.Quantity = -1;

            var result = validator.TestValidate(dto);

            result.ShouldHaveValidationErrorFor(x => x.Quantity);
        }

        [Fact]
        public void Validate_WhenLastUpdatedIsEmpty_ShouldHaveValidationError()
        {
            var context = new ShopProjectContext();
            var validator = new CreateInventoryValidator(context);

            var dto = CreateDto();
            dto.LastUpdated = default;

            var result = validator.TestValidate(dto);

            result.ShouldHaveValidationErrorFor(x => x.LastUpdated);
        }

        [Fact]
        public void Validate_WhenInventoryAlreadyExistsForProductAndStore_ShouldHaveValidationError()
        {
            var context = new ShopProjectContext();
            var existingInventory = context.Inventories.FirstOrDefault();

            Assert.NotNull(existingInventory);

            var validator = new CreateInventoryValidator(context);

            var dto = new InventoryDto
            {
                Quantity = 20,
                ProductId = existingInventory!.ProductId,
                StoreId = existingInventory.StoreId,
                LastUpdated = DateTime.Now
            };

            var result = validator.TestValidate(dto);

            result.ShouldHaveValidationErrorFor(x => x.ProductId);
        }
    }
}