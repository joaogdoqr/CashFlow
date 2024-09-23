using CashFlow.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CashFlow.Infrastructure.DataAccess
{
    internal class CashFlowDbContext(DbContextOptions options) : DbContext(options)
    {
        public DbSet<Expense> Expenses { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
