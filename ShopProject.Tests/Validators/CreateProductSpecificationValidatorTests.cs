using FluentValidation.TestHelper;
using ShopProject.Application.DataTransfer;
using ShopProject.DataAccess;
using ShopProject.Implementation.Validators;
using System;
using System.Collections.Generic;
using System.Text;
namespace ShopProject.Tests.Validators
{
    public class CreateProductSpecificationValidatorTests
    {
        private static ProductSpecificationDto CreateDto()
        {
            return new ProductSpecificationDto
            {
                ProductId = 999999,
                SpecificationName = "RAM",
                SpecificationValue = "8 GB"
            };
        }

        [Fact]
        public void Validate_WhenProductDoesNotExist_ShouldHaveValidationError()
        {
            var context = new ShopProjectContext();
            var validator = new CreateProductSpecificationValidator(context);

            var dto = CreateDto();

            var result = validator.TestValidate(dto);

            result.ShouldHaveValidationErrorFor(x => x.ProductId);
        }

        [Fact]
        public void Validate_WhenSpecificationNameIsEmpty_ShouldHaveValidationError()
        {
            var context = new ShopProjectContext();
            var validator = new CreateProductSpecificationValidator(context);

            var dto = CreateDto();
            dto.SpecificationName = "";

            var result = validator.TestValidate(dto);

            result.ShouldHaveValidationErrorFor(x => x.SpecificationName);
        }

        [Fact]
        public void Validate_WhenSpecificationNameIsTooShort_ShouldHaveValidationError()
        {
            var context = new ShopProjectContext();
            var validator = new CreateProductSpecificationValidator(context);

            var dto = CreateDto();
            dto.SpecificationName = "a";

            var result = validator.TestValidate(dto);

            result.ShouldHaveValidationErrorFor(x => x.SpecificationName);
        }

        [Fact]
        public void Validate_WhenSpecificationValueIsEmpty_ShouldHaveValidationError()
        {
            var context = new ShopProjectContext();
            var validator = new CreateProductSpecificationValidator(context);

            var dto = CreateDto();
            dto.SpecificationValue = "";

            var result = validator.TestValidate(dto);

            result.ShouldHaveValidationErrorFor(x => x.SpecificationValue);
        }

        [Fact]
        public void Validate_WhenSpecificationValueIsTooShort_ShouldHaveValidationError()
        {
            var context = new ShopProjectContext();
            var validator = new CreateProductSpecificationValidator(context);

            var dto = CreateDto();
            dto.SpecificationValue = "a";

            var result = validator.TestValidate(dto);

            result.ShouldHaveValidationErrorFor(x => x.SpecificationValue);
        }

        [Fact]
        public void Validate_WhenSpecificationAlreadyExistsForProduct_ShouldHaveValidationError()
        {
            var context = new ShopProjectContext();
            var validator = new CreateProductSpecificationValidator(context);

            var dto = CreateDto();

            var result = validator.TestValidate(dto);

            result.ShouldHaveValidationErrorFor(x => x.SpecificationName);
        }
    }
}
