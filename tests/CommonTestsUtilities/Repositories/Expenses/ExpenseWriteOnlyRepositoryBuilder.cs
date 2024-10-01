using CashFlow.Domain.Repositories.Expenses;
using Moq;

namespace CommonTestsUtilities.Repositories.Expenses
{
    public class ExpenseWriteOnlyRepositoryBuilder
    {
        public static IExpensesWriteOnlyRepository Build()
        {
            var mock = new Mock<IExpensesWriteOnlyRepository>();

            return mock.Object;
        }
    }
}
