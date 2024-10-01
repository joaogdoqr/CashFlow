using CashFlow.Communication.Requests.Expenses;

namespace CashFlow.Application.UseCases.Expenses.Update
{
    public interface IUpdateExpenseUseCase
    {
        Task Execute(long id, RequestExpense request);
    }
}
