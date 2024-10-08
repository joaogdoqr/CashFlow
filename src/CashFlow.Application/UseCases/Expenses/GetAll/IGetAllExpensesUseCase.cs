using CashFlow.Communication.Responses.Expense;

namespace CashFlow.Application.UseCases.Expenses.GetAll
{
    public interface IGetAllExpensesUseCase
    {
        Task<ResponseExpenses> Execute();
    }
}
