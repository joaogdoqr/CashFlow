using CashFlow.Application.UseCases.Users.ChangePassoword;
using CashFlow.Domain.Entities;
using CashFlow.Exception;
using CashFlow.Exception.ExceptionsBase;
using CommonTestsUtilities.Cryptography;
using CommonTestsUtilities.Entities;
using CommonTestsUtilities.Repositories;
using CommonTestsUtilities.Repositories.Users;
using CommonTestsUtilities.Requests.Users;
using FluentAssertions;

namespace UseCases.Tests.Users.ChangePassword
{
    public class ChangePasswordUseCaseTest
    {
        [Fact]
        public async Task Success()
        {
            var user = UserBuilder.Build();
            var request = RequestChangePasswordBuilder.Build();
            var useCase = CreateUseCase(user, request.Password);

            var act = async () => await useCase.Execute(request);

            await act.Should().NotThrowAsync();
        }

        [Fact]
        public async Task Error_NewPassword_Empty()
        {
            var user = UserBuilder.Build();
            var request = RequestChangePasswordBuilder.Build();
            request.NewPassword = string.Empty;
            var useCase = CreateUseCase(user, request.Password);

            var act = async () => await useCase.Execute(request);

            await act.Should().ThrowAsync<ErrorOnValidationException>()
                .Where(ex => ex.GetErros().Count == 1 && ex.GetErros().Contains(ResourceErrorMessages.INVALID_PASSWORD));
        }

        [Fact]
        public async Task Error_Passowrd_Dont_Match()
        {
            var user = UserBuilder.Build();
            var request = RequestChangePasswordBuilder.Build();
            var useCase = CreateUseCase(user);

            var act = async () => await useCase.Execute(request);

            var result = await act.Should().ThrowAsync<ErrorOnValidationException>();

            result.Where(e => e.GetErros().Count == 1 &&
                e.GetErros().Contains(ResourceErrorMessages.INVALID_PASSWORD));
        }

        private static ChangePasswordUseCase CreateUseCase(User user, string? password = null)
        {
            var loggedUser = LoggedUserBuilder.Build(user);
            var updateReadOnlyRepository = UserUpdateOnlyRepositoryBuilder.Build(user);
            var unitOfWork = UnitOfWorkBuilder.Build();
            var passwordEncrypter = new PasswordEncrypterBuilder().Verify(password).Build();

            return new ChangePasswordUseCase(loggedUser, updateReadOnlyRepository, unitOfWork, passwordEncrypter);
        }
    }
}
