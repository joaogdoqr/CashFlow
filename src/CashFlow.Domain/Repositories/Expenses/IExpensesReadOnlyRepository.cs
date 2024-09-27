using CashFlow.Domain.Entities;

namespace CashFlow.Domain.Repositories.Expenses
{
    public interface IExpensesReadOnlyRepository
    {
        public Task<List<Expense>> GetAllByUser(User user);

        public Task<Expense?> GetByIdAndUser(long id, User user);
    }
}
