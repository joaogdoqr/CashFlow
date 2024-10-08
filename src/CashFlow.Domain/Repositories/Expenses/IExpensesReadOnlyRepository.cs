using CashFlow.Domain.Entities;

namespace CashFlow.Domain.Repositories.Expenses
{
    public interface IExpensesReadOnlyRepository
    {
        public Task<List<Expense>> GetAll(User user);

        public Task<Expense?> GetById(long id, User user);

        public Task<List<Expense>> FilterByDate(User user, DateOnly date);
    }
}
