using Bogus;
using CashFlow.Application.UseCases.Expenses.GetById;
using CashFlow.Communication.Enums;
using CashFlow.Domain.Entities;
using CashFlow.Exception;
using CashFlow.Exception.ExceptionsBase;
using CommonTestsUtilities.Entities;
using CommonTestsUtilities.Mapper;
using CommonTestsUtilities.Repositories;
using CommonTestsUtilities.Repositories.Expenses;
using FluentAssertions;

namespace UseCases.Tests.Expenses.GetById
{
    public class GetByIdUseCaseTest
    {
        [Fact]
        public async Task Success()
        {
            var loggedUser = UserBuilder.Build();
            var expense = ExpenseBuilder.Build(loggedUser);

            var useCase = CreateUseCase(loggedUser, expense);

            var result = await useCase.Execute(expense.Id);

            result.Should().NotBeNull();
            result.Id.Should().Be(expense.Id);
            result.Title.Should().Be(expense.Title);
            result.Description.Should().Be(expense.Description);
            result.Date.Should().Be(expense.Date);
            result.Amount.Should().Be(expense.Amount);
            result.PaymentType.Should().Be((PaymentType)expense.PaymentType);
            result.Tags.Should().NotBeNullOrEmpty().And.BeEquivalentTo(expense.Tags.Select(tag => tag.Name));
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
        
        private static GetByIdUseCase CreateUseCase(User user, Expense? expense = null)
        {
            var repository = new ExpensesReadOnlyRepositoryBuilder().GetById(user, expense).Build();
            var mapper = MapperBuilder.Build();
            var loggedUser = LoggedUserBuilder.Build(user);

            return new GetByIdUseCase(repository, loggedUser, mapper);
        }
    } 
}
