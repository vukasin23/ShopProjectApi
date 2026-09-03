using FluentValidation.TestHelper;
using ShopProject.Application.DataTransfer;
using ShopProject.DataAccess;
using ShopProject.Implementation.Validators;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopProject.Tests.Validators
{
    public class CreateProductValidatorTests
    {
        [Fact]
        public void Validate_WhenNameIsNotUnique_ShouldHaveError()
        {
            // Arrange
            var context = new ShopProjectContext();
            var validator = new CreateProductValidator(context);
            var dto = new ProductDto
            {
                Name = "Proizvod 1",
                Description = "Test description",
                Price = 100,
                CategoryId = 1
            };
            // Act
            var result = validator.TestValidate(dto);
            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Name);
        }

        [Fact]
        public void Validate_WhenCategoryDoesNotExist_ShouldHaveError()
        {
            // Arrange
            var context = new ShopProjectContext();
            var validator = new CreateProductValidator(context);
            var dto = new ProductDto
            {
                Name = "Proizvod 1",
                Description = "Test description",
                Price = 100,
                CategoryId = 5
            };
            // Act
            var result = validator.TestValidate(dto);
            // Assert
            result.ShouldHaveValidationErrorFor(x => x.CategoryId);
        }

        [Fact]
        public void Validate_WhenCategoryIdIsNotEntered_ShouldHaveError()
        {
            // Arrange
            var context = new ShopProjectContext();
            var validator = new CreateProductValidator(context);
            var dto = new ProductDto
            {
                Name = "Proizvod 1",
                Description = "Test description",
                Price = 100,
                CategoryId = 0
            };
            // Act
            var result = validator.TestValidate(dto);
            // Assert
            result.ShouldHaveValidationErrorFor(x => x.CategoryId);
        }

        [Fact]
        public void Validate_WhenPriceIsNegative_ShouldHaveError()
        {
            // Arrange
            var context = new ShopProjectContext();
            var validator = new CreateProductValidator(context);
            var dto = new ProductDto
            {
                Name = "Proizvod 1",
                Description = "Test description",
                Price = -100,
                CategoryId = 0
            };
            // Act
            var result = validator.TestValidate(dto);
            // Assert
            result.ShouldHaveValidationErrorFor(x => x.CategoryId);
        }
    }
}
