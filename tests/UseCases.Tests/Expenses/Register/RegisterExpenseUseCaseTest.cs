using CashFlow.Application.UseCases.Expenses.Register;
using CashFlow.Domain.Entities;
using CashFlow.Exception;
using CashFlow.Exception.ExceptionsBase;
using CommonTestsUtilities.Entities;
using CommonTestsUtilities.Mapper;
using CommonTestsUtilities.Repositories;
using CommonTestsUtilities.Repositories.Expenses;
using CommonTestsUtilities.Requests.Expenses;
using FluentAssertions;

namespace UseCases.Tests.Expenses.Register
{
    public class RegisterExpenseUseCaseTest
    {
        [Fact]
        public async Task Success()
        {
            var loggedUser = UserBuilder.Build();

            var request = RequestExpenseBuilder.Build();

            var useCase = CreateUseCase(loggedUser);

            var result = await useCase.Execute(request);

            result.Should().NotBeNull();
            result.Title.Should().Be(request.Title);
        }

        [Fact]
        public async Task Error_Title_Empty()
        {
            var loggedUser = UserBuilder.Build();
            
            var request = RequestExpenseBuilder.Build();
            request.Title = string.Empty;

            var useCase = CreateUseCase(loggedUser);

            var act = async () => await useCase.Execute(request);

            var result = await act.Should().ThrowAsync<ErrorOnValidationException>();

            result.Where(ex => ex.GetErros().Count == 1 && ex.GetErros().Contains(ResourceErrorMessages.TITLE_REQUIRED));
        }

        private static RegisterExpenseUseCase CreateUseCase(User user)
        {
            var repository = ExpenseWriteOnlyRepositoryBuilder.Build();
            var loggedUser = LoggedUserBuilder.Build(user);
            var unitOfWork = UnitOfWorkBuilder.Build();
            var mapper = MapperBuilder.Build();

            return new RegisterExpenseUseCase(repository, loggedUser, unitOfWork, mapper);
        }
    }
}
