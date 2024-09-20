using CashFlow.Communication.Responses;

namespace CashFlow.Application.UseCases.Expenses.GetById
{
    public interface IGetByIdUseCase
    {
        Task<ResponseExpense> Execute(long id);
    }
}
