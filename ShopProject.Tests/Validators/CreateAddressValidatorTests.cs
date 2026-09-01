using FluentValidation.TestHelper;
using ShopProject.Application.DataTransfer;
using ShopProject.DataAccess;
using ShopProject.Implementation.Validators;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopProject.Tests.Validators
{
    public class CreateAddressValidatorTests
    {

        //private readonly ShopProjectContext _context;

        //public CreateAddressValidatorTests(ShopProjectContext context)
        //{
        //    _context = context;
        //}

        [Fact]
        public void Validate_WhenUserAlreadyHasAddress_ShouldHaveValidationError()
        {
            //Arrange
            var context = new ShopProjectContext();

            var validator = new CreateAddressValidator(context);

            var dto = new AddressDto
            {
                UserId = 3,
                Street = "Knez Mihailova 18",
                City = "Belgrade",
                ZipCode = "11000",
                Country = "Serbia",
                State = "Zeleni venac"
            };

            var result = validator.TestValidate(dto);

            result.ShouldHaveValidationErrorFor(x=>x.UserId);
        }

        [Fact]
        public void Valide_WhenUserDoesNotExist_ShouldHaveValidationError() {
            //Arrange
            var context = new ShopProjectContext();

            var validator = new CreateAddressValidator(context);

            var dto = new AddressDto
            {
                UserId = 2,
                Street = "Knez Mihailova 18",
                City = "Belgrade",
                ZipCode = "11000",
                Country = "Serbia",
                State = "Zeleni venac"
                
            };

            var result = validator.TestValidate(dto);

            result.ShouldHaveValidationErrorFor(x => x.UserId);
        }

    }
}
