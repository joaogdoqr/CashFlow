﻿using CashFlow.Application.UseCases.Users.Register;
using CashFlow.Exception;
using CommonTestsUtilities.Requests.Users;
using FluentAssertions;

namespace Validators.Tests.User.Register
{
    public class RegisterUserValidatorTest
    {
        [Fact]
        public void Success()
        {
            var validator = new RegisterUserValidator();
            var request = RequestRegisterUserBuilder.Build();
            
            var result = validator.Validate(request);

            result.IsValid.Should().BeTrue();
        }

        [Theory]
        [InlineData("")]
        [InlineData("       ")]
        [InlineData(null)]
        public void Error_Name_Empty(string name)
        {
            var validator = new RegisterUserValidator();
            var request = RequestRegisterUserBuilder.Build();
            request.Name = name;

            var result = validator.Validate(request);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceErrorMessages.NAME_EMPTY));
        }

        [Theory]
        [InlineData("")]
        [InlineData("       ")]
        [InlineData(null)]
        public void Error_Email_Empty(string email)
        {
            var validator = new RegisterUserValidator();
            var request = RequestRegisterUserBuilder.Build();
            request.Email = email;

            var result = validator.Validate(request);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceErrorMessages.EMAIL_EMPTY));
        }

        [Fact]
        public void Error_Email_Invalid()
        {
            var validator = new RegisterUserValidator();
            var request = RequestRegisterUserBuilder.Build();
            request.Email = request.Email.Replace("@", "");

            var result = validator.Validate(request);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceErrorMessages.EMAIL_INVALID));
        }

        [Fact]
        public void Error_Password_Empty()
        {
            var validator = new RegisterUserValidator();
            var request = RequestRegisterUserBuilder.Build();
            request.Password = string.Empty;

            var result = validator.Validate(request);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceErrorMessages.INVALID_PASSWORD));
        }
    }
}
