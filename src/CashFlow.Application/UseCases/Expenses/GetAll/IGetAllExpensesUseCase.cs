using CashFlow.Communication.Responses.Expenses;

namespace CashFlow.Application.UseCases.Expenses.GetAll
{
    public interface IGetAllExpensesUseCase
    {
        Task<ResponseExpenses> Execute();
    }
}
