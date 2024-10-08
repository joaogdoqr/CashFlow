using CashFlow.Domain.Entities;
using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Infrastructure.DataAccess.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace CashFlow.Infrastructure.DataAccess.Repositories
{
    internal class ExpensesRepository(CashFlowDbContext dbContext) : IExpensesReadOnlyRepository, IExpensesWriteOnlyRepository, IExpensesUpdateOnlyRepository
    {
        private readonly CashFlowDbContext _dbContext = dbContext;

        public async Task Add(Expense expense)
        {
            await _dbContext.AddAsync(expense);
        }

        public async Task Delete(long id)
        {
            var expense = await _dbContext.Expenses.FirstAsync(expense => expense.Id == id);

            _dbContext.Expenses.Remove(expense);
        }

        public async Task<List<Expense>> GetAll(User user)
        {
            return await _dbContext.Expenses.AsNoTracking()
                .Where(expense => expense.UserId == user.Id)
                .ToListAsync();
        }

        async Task<Expense?> IExpensesReadOnlyRepository.GetById(long id, User user)
        {
            return await GetFullExpenseAsync()
                .AsNoTracking()
                .FirstOrDefaultAsync(expense => expense.Id == id && expense.UserId == user.Id);
        }

        async Task<Expense?> IExpensesUpdateOnlyRepository.GetById(long id, User user)
        {
            return await GetFullExpenseAsync()
                .FirstOrDefaultAsync(expense => expense.Id == id && expense.UserId == user.Id);
        }

        public void Update(Expense expense)
        {
            _dbContext.Expenses.Update(expense);
        }

        public async Task<List<Expense>> FilterByDate(User user, DateOnly date)
        {
            var daysInMonth = DateTime.DaysInMonth(date.Year, date.Month);
            var startDate = new DateTime(date.Year, date.Month, 1).Date;
            var endDate = new DateTime(date.Year, date.Month, daysInMonth, 23, 59, 59).Date;

            return await _dbContext.Expenses
                .AsNoTracking()
                .Where(expense => expense.UserId == user.Id && expense.Date >= startDate && expense.Date <= endDate)
                .OrderBy(expense => expense.Date)
                .ThenBy(expense => expense.Title)
                .ToListAsync();
        }

        private IIncludableQueryable<Expense, ICollection<Tag>> GetFullExpenseAsync()
        {
            return _dbContext.Expenses
                .Include(expense => expense.Tags);
        }
    }
}
