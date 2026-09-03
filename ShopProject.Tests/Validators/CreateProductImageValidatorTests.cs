using FluentValidation.TestHelper;
using ShopProject.Application.DataTransfer;
using ShopProject.DataAccess;
using ShopProject.Implementation.Validators;
using System;
using System.Collections.Generic;
using System.Text;
namespace ShopProject.Tests.Validators
{
    public class CreateProductImageValidatorTests
    {
        private static ProductImageDto CreateDto()
        {
            return new ProductImageDto
            {
                ProductId = 999999,
                ImageUrl = "product-image.jpg",
                AltText = "Main product image",
                IsPrimary = false
            };
        }

        [Fact]
        public void Validate_WhenProductDoesNotExist_ShouldHaveValidationError()
        {
            var context = new ShopProjectContext();
            var validator = new CreateProductImageValidator(context);

            var dto = CreateDto();

            var result = validator.TestValidate(dto);

            result.ShouldHaveValidationErrorFor(x => x.ProductId);
        }

        [Fact]
        public void Validate_WhenImageUrlIsEmpty_ShouldHaveValidationError()
        {
            var context = new ShopProjectContext();
            var validator = new CreateProductImageValidator(context);

            var dto = CreateDto();
            dto.ImageUrl = "";

            var result = validator.TestValidate(dto);

            result.ShouldHaveValidationErrorFor(x => x.ImageUrl);
        }

        [Theory]
        [InlineData("image.txt")]
        [InlineData("image.pdf")]
        [InlineData("image.docx")]
        [InlineData("image")]
        public void Validate_WhenImageExtensionIsInvalid_ShouldHaveValidationError(string imageUrl)
        {
            var context = new ShopProjectContext();
            var validator = new CreateProductImageValidator(context);

            var dto = CreateDto();
            dto.ImageUrl = imageUrl;

            var result = validator.TestValidate(dto);

            result.ShouldHaveValidationErrorFor(x => x.ImageUrl);
        }

        [Theory]
        [InlineData("image.jpg")]
        [InlineData("image.jpeg")]
        [InlineData("image.png")]
        [InlineData("image.webp")]
        [InlineData("image.gif")]
        public void Validate_WhenImageExtensionIsValid_ShouldNotHaveValidationError(string imageUrl)
        {
            var context = new ShopProjectContext();
            var validator = new CreateProductImageValidator(context);

            var dto = CreateDto();
            dto.ImageUrl = imageUrl;

            var result = validator.TestValidate(dto);

            result.ShouldNotHaveValidationErrorFor(x => x.ImageUrl);
        }

        [Fact]
        public void Validate_WhenAltTextIsEmpty_ShouldHaveValidationError()
        {
            var context = new ShopProjectContext();
            var validator = new CreateProductImageValidator(context);

            var dto = CreateDto();
            dto.AltText = "";

            var result = validator.TestValidate(dto);

            result.ShouldHaveValidationErrorFor(x => x.AltText);
        }

        [Fact]
        public void Validate_WhenAltTextIsTooShort_ShouldHaveValidationError()
        {
            var context = new ShopProjectContext();
            var validator = new CreateProductImageValidator(context);

            var dto = CreateDto();
            dto.AltText = "ab";

            var result = validator.TestValidate(dto);

            result.ShouldHaveValidationErrorFor(x => x.AltText);
        }
    }
}
