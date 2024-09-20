using CashFlow.Domain.Entities;

namespace CashFlow.Domain.Repositories.Expenses
{
    public interface  IExpensesWriteOnlyRepository
    {
        Task Add(Expense expense);

        /// <summary>
        /// Returns true if deleted successfully
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> Delete(long id);
    }
}
