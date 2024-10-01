using CashFlow.Application.UseCases.Expenses.GetAll;
using CashFlow.Domain.Entities;
using CommonTestsUtilities.Entities;
using CommonTestsUtilities.Mapper;
using CommonTestsUtilities.Repositories;
using CommonTestsUtilities.Repositories.Expenses;
using FluentAssertions;

namespace UseCases.Tests.Expenses.GetAll
{
    public class GetAllExpensesUseCaseTest
    {
        [Fact]
        public async Task Success()
        {
            var loggedUser = UserBuilder.Build();
            var expenses = ExpenseBuilder.Collection(loggedUser);

            var useCase = CreateUseCase(loggedUser, expenses);

            var result = await useCase.Execute();

            result.Should().NotBeNull();
            result.Expenses.Should().NotBeNullOrEmpty().And.AllSatisfy(expense =>
            {
                expense.Id.Should().BeGreaterThan(0);
                expense.Title.Should().NotBeNullOrWhiteSpace();
                expense.Amount.Should().BeGreaterThan(0);
            });
        }

        private static GetAllExpensesUseCase CreateUseCase(User user, List<Expense> expenses)
        {
            var repository = new ExpensesReadOnlyRepositoryBuilder().GetAll(user, expenses).Build();
            var loggedUser = LoggedUserBuilder.Build(user);
            var mapper = MapperBuilder.Build();

            return new GetAllExpensesUseCase(repository, loggedUser, mapper);
        }
    }
}
