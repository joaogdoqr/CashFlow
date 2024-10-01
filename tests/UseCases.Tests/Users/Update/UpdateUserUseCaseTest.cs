using CashFlow.Application.UseCases.Users.Register;
using CashFlow.Application.UseCases.Users.Update;
using CashFlow.Domain.Entities;
using CashFlow.Exception;
using CashFlow.Exception.ExceptionsBase;
using CommonTestsUtilities.Cryptography;
using CommonTestsUtilities.Entities;
using CommonTestsUtilities.Mapper;
using CommonTestsUtilities.Repositories;
using CommonTestsUtilities.Repositories.Users;
using CommonTestsUtilities.Requests.Users;
using CommonTestsUtilities.Token;
using FluentAssertions;

namespace UseCases.Tests.Users.Update
{
    public class UpdateUserUseCaseTest
    {
        [Fact]
        public async Task Success()
        {
            var user = UserBuilder.Build();
            var request = RequestUpdateUserBuilder.Build();
            var useCase = CreateUseCase(user);

            var act = async () => await useCase.Execute(request);

            await act.Should().NotThrowAsync();

            user.Name.Should().Be(request.Name);
            user.Email.Should().Be(request.Email);
        }

        [Fact]
        public async Task Error_Name_Empty()
        {
            var user = UserBuilder.Build();
            var request = RequestUpdateUserBuilder.Build();
            request.Name = string.Empty;
            var useCase = CreateUseCase(user);

            var act = async () => await useCase.Execute(request);

            await act.Should().ThrowAsync<ErrorOnValidationException>()
                .Where(ex => ex.GetErros().Count == 1 && ex.GetErros().Contains(ResourceErrorMessages.NAME_EMPTY));
        }

        [Fact]
        public async Task Error_Email_Already_Exist()
        {
            var user = UserBuilder.Build();
            var request = RequestUpdateUserBuilder.Build();
            var useCase = CreateUseCase(user, request.Email);

            var act = async () => await useCase.Execute(request);

            var result = await act.Should().ThrowAsync<ErrorOnValidationException>(ResourceErrorMessages.EMAIL_ALREADY_REGISTERED)
                .Where(ex => ex.GetErros().Count == 1 && ex.GetErros().Contains(ResourceErrorMessages.EMAIL_ALREADY_REGISTERED));
        }

        private static UpdateUserProfileUseCase CreateUseCase(User user, string? email = null)
        {
            var updateOnlyRepository = UserUpdateOnlyRepositoryBuilder.Build(user);
            var loggedUser = LoggedUserBuilder.Build(user);
            var readOnlyRepository = new UserReadOnlyRepositoryBuilder();
            var unitOfWork = UnitOfWorkBuilder.Build();
            var mapper = MapperBuilder.Build();

            if (!string.IsNullOrEmpty(email))
                readOnlyRepository.ExistActiveUserWithEmail(email);

            return new UpdateUserProfileUseCase(loggedUser, updateOnlyRepository, readOnlyRepository.Build(), unitOfWork, mapper);
        }
    }
}
