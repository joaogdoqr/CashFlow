using CashFlow.Domain.Entities;

namespace WebApi.Tests.Resources
{
    public  class ExpenseIdentityManager(Expense expense)
    {
        private readonly Expense _expense = expense;

        public long GetId() => _expense.Id;

        public DateTime GetDate() => _expense.Date;
    }
}
