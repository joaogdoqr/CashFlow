using CashFlow.Application.UseCases.Users;
using CashFlow.Communication.Requests.Users;
using CommonTestsUtilities.Requests.Users;
using FluentAssertions;
using FluentValidation;

namespace Validators.Tests.User
{
    public class RegisterUserValidatorTest
    {
        [Theory]
        [InlineData("")]
        [InlineData("  ")]
        [InlineData(null)]
        [InlineData("a")]
        [InlineData("aa")]
        [InlineData("aaa")]
        [InlineData("aaaa")]
        [InlineData("aaaaa")]
        [InlineData("aaaaaa")]
        [InlineData("aaaaaaa")]
        [InlineData("aaaaaaaa")]
        [InlineData("AAAAAAAA")]
        [InlineData("aaaaaaaA")]
        [InlineData("aaaaaa1A")]
        public void Error_Password_Invalid(string password)
        {
            // Arrange 
            var validator = new PasswordValidator<RequestRegisterUser>();
            var request = RequestRegisterUserBuilder.Build();

            // Act
            var result = validator.IsValid(new ValidationContext<RequestRegisterUser>(request), password);

            // Assert
            result.Should().BeFalse();
        }
    }
}
