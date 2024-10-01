using CashFlow.Application.UseCases.Users.Profile;
using CashFlow.Communication.Responses.User;
using CashFlow.Domain.Entities;
using CommonTestsUtilities.Entities;
using CommonTestsUtilities.Mapper;
using CommonTestsUtilities.Repositories;
using FluentAssertions;

namespace UseCases.Tests.Users.Profile
{
    public class GetUserProfileUseCaseTest
    {
        [Fact]
        public async Task Success()
        {
            var user = UserBuilder.Build();
            var useCase = CreateUseCase(user);

            var result = await useCase.Execute();

            result.Should().NotBeNull();
            result.Name.Should().Be(user.Name);
            result.Email.Should().Be(user.Email);
        }

        private static GetUserProfileUseCase CreateUseCase(User user)
        {
            var loggedUser = LoggedUserBuilder.Build(user);
            var mapper = MapperBuilder.Build();

            return new GetUserProfileUseCase(loggedUser, mapper);
        }
    }
}
