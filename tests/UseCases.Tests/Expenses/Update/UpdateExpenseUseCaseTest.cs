using Bogus;
using CashFlow.Application.UseCases.Expenses.Update;
using CashFlow.Communication.Enums;
using CashFlow.Communication.Requests;
using CashFlow.Domain.Entities;
using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Exception;
using CashFlow.Exception.ExceptionsBase;
using CommonTestsUtilities.Entities;
using CommonTestsUtilities.Mapper;
using CommonTestsUtilities.Repositories;
using CommonTestsUtilities.Repositories.Expenses;
using CommonTestsUtilities.Requests.Expenses;
using FluentAssertions;

namespace UseCases.Tests.Expenses.Update
{
    public class UpdateExpenseUseCaseTest
    {
        [Fact]
        public async Task Success()
        {
            var loggedUser = UserBuilder.Build();
            var expense = ExpenseBuilder.Build(loggedUser);
            var request = RequestExpenseBuilder.Build();

            var useCase = CreateUseCase(loggedUser, expense);

            var act = async () => await useCase.Execute(expense.Id, request);

            await act.Should().NotThrowAsync();

            expense.Title.Should().Be(request.Title);
            expense.Description.Should().Be(request.Description);
            expense.Date.Should().Be(request.Date);
            expense.Amount.Should().Be(request.Amount);
            expense.PaymentType.Should().Be((CashFlow.Domain.Enums.PaymentType)request.PaymentType);
        }

        [Fact]
        public async Task Error_Title_Empty()
        {
            var loggedUser = UserBuilder.Build();
            var expense = ExpenseBuilder.Build(loggedUser);
            var request = RequestExpenseBuilder.Build();
            request.Title = string.Empty;

            var useCase = CreateUseCase(loggedUser);

            var act = async () => await useCase.Execute(expense.Id, request);

            var result = await act.Should().ThrowAsync<ErrorOnValidationException>();

            result.Where(ex => ex.GetErros().Count == 1 && ex.GetErros().Contains(ResourceErrorMessages.TITLE_REQUIRED));
        }

        [Fact]
        public async Task Error_Expense_Not_Found()
        {
            var loggedUser = UserBuilder.Build();
            var request = RequestExpenseBuilder.Build();
            var id = new Faker().Random.Int(min: 99999);

            var useCase = CreateUseCase(loggedUser);

            var act = async () => await useCase.Execute(id, request);

            var result = await act.Should().ThrowAsync<NotFoundException>();

            result.Where(ex => ex.GetErros().Count == 1 && ex.GetErros().Contains(ResourceErrorMessages.EXPENSE_NOT_FOUND));
        }

        private static UpdateExpenseUseCase CreateUseCase(User user, Expense? expense = null)
        {
            var repository = new ExpensesUpdateOnlyRepositoryBuilder().GetById(user, expense).Build();
            var loggedUser = LoggedUserBuilder.Build(user);
            var unitOfWork = UnitOfWorkBuilder.Build();
            var mapper = MapperBuilder.Build();

            return new UpdateExpenseUseCase(repository, loggedUser, unitOfWork, mapper);
        }
    }
}
