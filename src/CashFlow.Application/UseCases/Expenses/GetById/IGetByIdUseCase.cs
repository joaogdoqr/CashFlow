using CashFlow.Communication.Responses.Expenses;

namespace CashFlow.Application.UseCases.Expenses.GetById
{
    public interface IGetByIdUseCase
    {
        Task<ResponseExpense> Execute(long id);
    }
}
