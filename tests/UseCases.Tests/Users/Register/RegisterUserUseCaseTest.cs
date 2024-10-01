using CashFlow.Application.UseCases.Users.Register;
using CashFlow.Exception;
using CashFlow.Exception.ExceptionsBase;
using CommonTestsUtilities.Cryptography;
using CommonTestsUtilities.Mapper;
using CommonTestsUtilities.Repositories;
using CommonTestsUtilities.Repositories.Users;
using CommonTestsUtilities.Requests.Users;
using CommonTestsUtilities.Token;
using FluentAssertions;

namespace UseCases.Tests.Users.Register
{
    public class RegisterUserUseCaseTest
    {
        [Fact]
        public async Task Success()
        {
            var request = RequestRegisterUserBuilder.Build();
            var useCase = CreateUseCase();

            var result = await useCase.Execute(request);

            result.Should().NotBeNull();
            result.Name.Should().Be(request.Name);
            result.Token.Should().NotBeNull();
        }

        [Fact]
        public async Task Error_Name_Empty()
        {
            var request = RequestRegisterUserBuilder.Build();
            request.Name = string.Empty;

            var useCase = CreateUseCase();

            var act = async () => await useCase.Execute(request);

            await act.Should().ThrowAsync<ErrorOnValidationException>()
                .Where(ex => ex.GetErros().Count == 1 && ex.GetErros().Contains(ResourceErrorMessages.NAME_EMPTY));
        }

        [Fact]
        public async Task Error_Email_Already_Exist()
        {
            var request = RequestRegisterUserBuilder.Build();

            var useCase = CreateUseCase(request.Email);

            var act = async () => await useCase.Execute(request);

            var result = await act.Should().ThrowAsync<ErrorOnValidationException>(ResourceErrorMessages.EMAIL_ALREADY_REGISTERED)
                .Where(ex => ex.GetErros().Count == 1 && ex.GetErros().Contains(ResourceErrorMessages.EMAIL_ALREADY_REGISTERED));
        }

        private static RegisterUserUseCase CreateUseCase(string? email = null)
        {
            var readOnlyRepository = new UserReadOnlyRepositoryBuilder();
            var writeOnlyRepository = UserWriteOnlyRepositoryBuilder.Build();
            var unitOfWork = UnitOfWorkBuilder.Build();
            var mapper = MapperBuilder.Build();
            var passwordEncrypter = new PasswordEncrypterBuilder().Build();
            var jwtGenerator = JwtTokenGeneratorBuilder.Build();

            if (!string.IsNullOrEmpty(email)) 
                readOnlyRepository.ExistActiveUserWithEmail(email);

            return new RegisterUserUseCase(readOnlyRepository.Build(), writeOnlyRepository, unitOfWork, mapper, passwordEncrypter, jwtGenerator);
        }
    }
}
