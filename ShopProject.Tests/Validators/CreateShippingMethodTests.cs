using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation.TestHelper;
using ShopProject.Application.DataTransfer;
using ShopProject.DataAccess;
using ShopProject.Implementation.Validators;
namespace ShopProject.Tests.Validators
{
    public class CreateShippingMethodTests
    {
        [Fact]
        public void Validate_WhenNameIsEmpty_ShouldHaveValidationError()
        {
            //Arrange
            var context = new ShopProjectContext();
            var validator = new CreateShippingMethodValidator(context);
            var dto = new ShippingMethodDto
            {
                Name = "",
                Price = 100,
                Description = "Fast shipping"
            };
            //Act
            var result = validator.TestValidate(dto);
            //Assert
            result.ShouldHaveValidationErrorFor(x => x.Name);
        }

        [Fact]
        public void Validate_WhenNameIsNotUnique_ShouldHaveValidationError()
        {
            //Arrange
            var context = new ShopProjectContext();
            var validator = new CreateShippingMethodValidator(context);
            var dto = new ShippingMethodDto
            {
                Name = "Delivery on house",
                Price = 100,
                Description = "Fast shipping"
            };
            //Act
            var result = validator.TestValidate(dto);
            //Assert
            result.ShouldHaveValidationErrorFor(x => x.Name);
        }
    }
}
