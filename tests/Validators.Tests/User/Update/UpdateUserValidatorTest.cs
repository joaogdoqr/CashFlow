using CashFlow.Application.UseCases.Users.Register;
using CashFlow.Application.UseCases.Users.Update;
using CashFlow.Exception;
using CommonTestsUtilities.Requests.Users;
using FluentAssertions;

namespace Validators.Tests.User.Update
{
    public class UpdateUserValidatorTest
    {
        [Fact]
        public void Success()
        {
            var validator = new UpdateUserValidator();
            var request = RequestUpdateUserBuilder.Build();

            var result = validator.Validate(request);

            result.IsValid.Should().BeTrue();
        }

        [Theory]
        [InlineData("")]
        [InlineData("       ")]
        [InlineData(null)]
        public void Error_Name_Empty(string name)
        {
            //Arrange
            var validator = new UpdateUserValidator();
            var request = RequestUpdateUserBuilder.Build();
            request.Name = name;

            //Act
            var result = validator.Validate(request);

            //Assert
            result.IsValid.Should().BeFalse();
            result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceErrorMessages.NAME_EMPTY));
        }

        [Theory]
        [InlineData("")]
        [InlineData("       ")]
        [InlineData(null)]
        public void Error_Email_Empty(string email)
        {
            //Arrange
            var validator = new UpdateUserValidator();
            var request = RequestUpdateUserBuilder.Build();
            request.Email = email;

            //Act
            var result = validator.Validate(request);

            //Assert
            result.IsValid.Should().BeFalse();
            result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceErrorMessages.EMAIL_EMPTY));
        }

        [Fact]
        public void Error_Email_Invalid()
        {
            //Arrange
            var validator = new UpdateUserValidator();
            var request = RequestUpdateUserBuilder.Build();
            request.Email = request.Email.Replace("@", "");

            //Act
            var result = validator.Validate(request);

            //Assert
            result.IsValid.Should().BeFalse();
            result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceErrorMessages.EMAIL_INVALID));
        }
    }
}
