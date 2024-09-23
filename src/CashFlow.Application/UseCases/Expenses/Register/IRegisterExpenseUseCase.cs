using CashFlow.Communication.Requests;
using CashFlow.Communication.Responses.Expenses;

namespace CashFlow.Application.UseCases.Expenses.Register
{
    public interface IRegisterExpenseUseCase
    {
        Task<ResponseRegisterExpense> Execute(RequestExpense request);
    }
}
