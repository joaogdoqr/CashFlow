using CashFlow.Domain.Entities;
using CashFlow.Domain.Repositories.Expenses;
using Moq;

namespace CommonTestsUtilities.Repositories.Expenses
{
    public class ExpensesReadOnlyRepositoryBuilder
    {
        private readonly Mock<IExpensesReadOnlyRepository> _repository = new();

        public ExpensesReadOnlyRepositoryBuilder GetAll(User user, List<Expense> expenses)
        {
            _repository.Setup(repository => repository.GetAll(user)).ReturnsAsync(expenses);

            return this;
        }

        public ExpensesReadOnlyRepositoryBuilder GetById(User user, Expense? expense)
        {
            if(expense is not null)
                _repository.Setup(repository => repository.GetById(expense.Id, user)).ReturnsAsync(expense);

            return this;
        }

        public ExpensesReadOnlyRepositoryBuilder FilterByDate(User user, List<Expense> expenses)
        {
            _repository.Setup(repository => repository.FilterByDate(user, It.IsAny<DateOnly>())).ReturnsAsync(expenses);

            return this;
        }

        public IExpensesReadOnlyRepository Build() => _repository.Object;
    }
}
