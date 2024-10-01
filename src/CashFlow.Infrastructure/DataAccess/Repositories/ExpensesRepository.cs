﻿using CashFlow.Domain.Entities;
using CashFlow.Domain.Repositories.Expenses;
using Microsoft.EntityFrameworkCore;

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
            return await _dbContext.Expenses.AsNoTracking()
                .FirstOrDefaultAsync(expense => expense.Id == id && expense.UserId == user.Id);
        }

        async Task<Expense?> IExpensesUpdateOnlyRepository.GetById(long id, User user)
        {
            return await _dbContext.Expenses
                .FirstOrDefaultAsync(expense => expense.Id == id && expense.UserId == user.Id);
        }

        public void Update(Expense expense)
        {
            _dbContext.Expenses.Update(expense);
        }
    }
}
