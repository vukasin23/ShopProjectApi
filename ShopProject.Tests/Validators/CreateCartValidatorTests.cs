using FluentValidation.TestHelper;
using ShopProject.Application.DataTransfer;
using ShopProject.DataAccess;
using ShopProject.Implementation;
using ShopProject.Implementation.Validators;
using System;
using System.Linq;
using Xunit;

namespace ShopProject.Tests.Validators
{
    public class CreateCartValidatorTests
    {
        [Fact]
        public void Validate_WhenActorDoesNotExist_ShouldHaveValidationError()
        {
            var context = new ShopProjectContext();
            var validator = new CreateCartValidator(context, new Actor { Id = 999999 });

            var result = validator.TestValidate(new CartDto());

            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.ErrorMessage == "User does not exist.");
        }

        [Fact]
        public void Validate_WhenActorIsUnauthorized_ShouldHaveValidationError()
        {
            var context = new ShopProjectContext();
            var validator = new CreateCartValidator(context, new UnauthorizedActor());

            var result = validator.TestValidate(new CartDto());

            Assert.False(result.IsValid);
        }

        [Fact]
        public void Validate_WhenActorAlreadyHasCart_ShouldHaveValidationError()
        {
            var context = new ShopProjectContext();
            var existingCart = context.Carts.FirstOrDefault();

            Assert.NotNull(existingCart);

            var validator = new CreateCartValidator(context, new Actor { Id = existingCart!.UserId });

            var result = validator.TestValidate(new CartDto());

            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.ErrorMessage == "This user already has a cart.");
        }

        [Fact]
        public void Validate_WhenActorExistsAndHasNoCart_ShouldNotHaveValidationError()
        {
            var context = new ShopProjectContext();
            var userWithoutCart = context.Users.FirstOrDefault(u => !context.Carts.Any(c => c.UserId == u.Id));

            Assert.NotNull(userWithoutCart);

            var validator = new CreateCartValidator(context, new Actor { Id = userWithoutCart!.Id });

            var result = validator.TestValidate(new CartDto());

            Assert.True(result.IsValid);
        }
    }
}
