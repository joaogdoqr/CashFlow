using CashFlow.Application.UseCases.Login;
using CashFlow.Domain.Entities;
using CashFlow.Exception;
using CashFlow.Exception.ExceptionsBase;
using CommonTestsUtilities.Cryptography;
using CommonTestsUtilities.Entities;
using CommonTestsUtilities.Repositories.Users;
using CommonTestsUtilities.Requests.Login;
using CommonTestsUtilities.Token;
using FluentAssertions;

namespace UseCases.Tests.Login
{
    public class LoginUseCaseTest
    {
        [Fact]
        public async Task Success()
        {
            var user = UserBuilder.Build();

            var request = RequestLoginBuilder.Build();
            request.Email = user.Email;

            var useCase = CreateUseCase(user, request.Password);

            var result = await useCase.Execute(request);

            result.Should().NotBeNull();
            result.Name.Should().Be(user.Name);
            result.Token.Should().NotBeNullOrWhiteSpace();
        }

        [Fact]
        public async Task Error_User_Not_Found()
        {
            var user = UserBuilder.Build();

            var request = RequestLoginBuilder.Build();

            var useCase = CreateUseCase(user, request.Password);

            var act = async () => await useCase.Execute(request);

            var result = await act.Should().ThrowAsync<InvalidLoginException>()
                .Where(ex => ex.GetErros().Count == 1 && ex.GetErros().Contains(ResourceErrorMessages.EMAIL_OR_PASSWORD_INVALID));
        }

        [Fact]
        public async Task Error_Password_Not_Match()
        {
            var user = UserBuilder.Build();

            var request = RequestLoginBuilder.Build();

            var useCase = CreateUseCase(user);

            var act = async () => await useCase.Execute(request);

            var result = await act.Should().ThrowAsync<InvalidLoginException>()
                .Where(ex => ex.GetErros().Count == 1 && ex.GetErros().Contains(ResourceErrorMessages.EMAIL_OR_PASSWORD_INVALID));
        }

        private static LoginUseCase CreateUseCase(User user, string? password = null)
        {
            var readOnlyRepository = new UserReadOnlyRepositoryBuilder().GetUserByEmail(user).Build();
            var passwordEncrypter = new PasswordEncrypterBuilder().Verify(password).Build();
            var jwtGenerator = JwtTokenGeneratorBuilder.Build();

            return new LoginUseCase(readOnlyRepository, passwordEncrypter, jwtGenerator);
        }
    }
}
