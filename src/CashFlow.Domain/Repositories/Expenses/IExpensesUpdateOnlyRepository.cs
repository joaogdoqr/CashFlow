using CashFlow.Domain.Entities;

namespace CashFlow.Domain.Repositories.Expenses
{
    public interface IExpensesUpdateOnlyRepository
    {
        Task<Expense?> GetById(long id, User user);

        void Update(Expense expense);
    }
}
