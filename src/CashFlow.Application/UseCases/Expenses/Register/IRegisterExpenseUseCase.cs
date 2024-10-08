using CashFlow.Communication.Requests.Expenses;
using CashFlow.Communication.Responses.Expense;

namespace CashFlow.Application.UseCases.Expenses.Register
{
    public interface IRegisterExpenseUseCase
    {
        Task<ResponseRegisterExpense> Execute(RequestExpense request);
    }
}
