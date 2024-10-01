using CashFlow.Application.UseCases.Users.ChangePassoword;
using CashFlow.Application.UseCases.Users.Register;
using CashFlow.Exception;
using CommonTestsUtilities.Requests.Users;
using FluentAssertions;

namespace Validators.Tests.User.ChangePassword
{
    public class ChangePasswordValidatorTest
    {
        [Fact]
        public void Success()
        {
            var validator = new ChangePasswordValidator();
            var request = RequestChangePasswordBuilder.Build();

            var result = validator.Validate(request);

            result.IsValid.Should().BeTrue();
        }

        [Theory]
        [InlineData("")]
        [InlineData("       ")]
        [InlineData(null)]
        public void Error_NewPassword_Empty(string newPassword)
        {
            var validator = new ChangePasswordValidator();
            var request = RequestChangePasswordBuilder.Build();
            request.NewPassword = newPassword;

            var result = validator.Validate(request);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceErrorMessages.INVALID_PASSWORD));
        }
    }
}
