using CashFlow.Application.UseCases.Expenses.Reports.Excel;
using CashFlow.Domain.Entities;
using CommonTestsUtilities.Entities;
using CommonTestsUtilities.Repositories;
using CommonTestsUtilities.Repositories.Expenses;
using FluentAssertions;

namespace UseCases.Tests.Expenses.Reports.Excel;
public class GenerateExpensesReportExcelUseCaseTest
{
    [Fact]
    public async Task Success()
    {
        var loggedUser = UserBuilder.Build();
        var expenses = ExpenseBuilder.Collection(loggedUser);

        var useCase = CreateUseCase(loggedUser, expenses);

        var result = await useCase.Execute(DateOnly.FromDateTime(DateTime.Today));

        result.Should().NotBeNullOrEmpty();
    }

    [Fact]
    public async Task Success_Empty()
    {
        var loggedUser = UserBuilder.Build();

        var useCase = CreateUseCase(loggedUser, []);

        var result = await useCase.Execute(DateOnly.FromDateTime(DateTime.Today));

        result.Should().BeEmpty();
    }

    private static GenerateExpensesReportExcelUseCase CreateUseCase(User user, List<Expense> expenses)
    {
        var repository = new ExpensesReadOnlyRepositoryBuilder().FilterByDate(user, expenses).Build();
        var loggedUser = LoggedUserBuilder.Build(user);

        return new GenerateExpensesReportExcelUseCase(repository, loggedUser);
    }
}
