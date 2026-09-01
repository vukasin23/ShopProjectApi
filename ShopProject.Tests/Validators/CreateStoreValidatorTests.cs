using FluentValidation.TestHelper;
using ShopProject.Application.DataTransfer;
using ShopProject.DataAccess;
using ShopProject.Implementation.Validators;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopProject.Tests.Validators
{
    public class CreateStoreValidatorTests
    {
        [Fact]
        public void Validate_WhenPhoneIsInvalid_ShouldHaveValidationError()
        { 
          //Arrange
          var context = new ShopProjectContext();
          var validator = new CreateStoreValidator(context);


            var dto = new StoreDto
            {
                Name = "Tech Store",
                Description = "Prodavnica tehnike",
                City = "Beograd",
                Address = "Knez Mihailova 10",
                Phone = "123"
            };

            var result = validator.TestValidate(dto);

            result  .ShouldHaveValidationErrorFor(x => x.Phone);
        }
    }
}
