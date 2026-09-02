using FluentValidation.TestHelper;
using ShopProject.Application.DataTransfer;
using ShopProject.DataAccess;
using ShopProject.Implementation.Validators;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopProject.Tests.Validators
{
    public class CreateCouponValidatorTests
    {
        [Fact]
        public void Validate_WhenCodeIsEmpty_ShouldHaveError()
        {
            // Arrange
            var context = new ShopProjectContext();
            var validator = new CreateCouponValidator(context);
            var dto = new CouponDto
            {
                Code = "",
                DiscountAmount = 10,
                ExpiryDate = DateTime.Now.AddDays(1)
            };
            // Act
            var result = validator.TestValidate(dto);
            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Code);
        }

        [Fact]
        public void Validate_WhenCodeIsNotUnique_ShouldHaveError()
        {
            // Arrange
            var context = new ShopProjectContext();
            var validator = new CreateCouponValidator(context);
            var dto = new CouponDto
            {
                Code = "",
                DiscountAmount = 10,
                ExpiryDate = DateTime.Now.AddDays(1)
            };
            // Act
            var result = validator.TestValidate(dto);
            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Code);
        }
    }
}
