using Bogus;
using CashFlow.Application.UseCases.Expenses.Delete;
using CashFlow.Domain.Entities;
using CashFlow.Exception;
using CashFlow.Exception.ExceptionsBase;
using CommonTestsUtilities.Entities;
using CommonTestsUtilities.Repositories;
using CommonTestsUtilities.Repositories.Expenses;
using FluentAssertions;

namespace WebApi.Tests.Expenses.Delete
{
    public class DeleteExpenseUseCaseTest
    {
        [Fact]
        public async Task Success()
        {
            var loggedUser = UserBuilder.Build();
            var expense = ExpenseBuilder.Build(loggedUser);

            var useCase = CreateUseCase(loggedUser, expense);

            var act = async () => await useCase.Execute(expense.Id);

            await act.Should().NotThrowAsync();
        }

        [Fact]
        public async Task Error_Expense_Not_Found()
        {
            var loggedUser = UserBuilder.Build();
            var id = new Faker().Random.Int(min: 99999);

            var useCase = CreateUseCase(loggedUser);

            var act = async () => await useCase.Execute(id);

            var result = await act.Should().ThrowAsync<NotFoundException>();

            result.Where(ex => ex.GetErros().Count == 1 && ex.GetErros().Contains(ResourceErrorMessages.EXPENSE_NOT_FOUND));
        }

        private static DeleteExpenseUseCase CreateUseCase(User user, Expense? expense = null)
        {
            var writeOnlyRepository = ExpenseWriteOnlyRepositoryBuilder.Build();
            var readOnlyRepository = new ExpensesReadOnlyRepositoryBuilder().GetById(user, expense).Build();
            var loggedUser = LoggedUserBuilder.Build(user);
            var unitOfWork = UnitOfWorkBuilder.Build();

            return new DeleteExpenseUseCase(writeOnlyRepository, readOnlyRepository, loggedUser, unitOfWork);
        }
    }
}
