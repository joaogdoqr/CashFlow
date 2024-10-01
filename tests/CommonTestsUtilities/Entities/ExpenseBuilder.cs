using Bogus;
using CashFlow.Domain.Entities;
using CashFlow.Domain.Enums;

namespace CommonTestsUtilities.Entities
{
    public class ExpenseBuilder
    {
        public static List<Expense> Collection(User user, uint count = 2)
        {
            var list = new List<Expense>();

            for(int i = 0; i < count; i++)
            {
                var expense = Build(user);
                expense.UserId = user.Id;

                list.Add(expense);
            }

            return list;
        }

        public static Expense Build(User user)
        {
            return new Faker<Expense>()
                .RuleFor(u => u.Id, faker => faker.Random.Int(min: 1))
                .RuleFor(u => u.Title, faker => faker.Commerce.ProductName())
                .RuleFor(r => r.Description, faker => faker.Commerce.ProductDescription())
                .RuleFor(r => r.Date, faker => faker.Date.Past())
                .RuleFor(r => r.Amount, faker => faker.Random.Decimal(min: 1, max: 1000))
                .RuleFor(r => r.PaymentType, faker => faker.PickRandom<PaymentType>())
                .RuleFor(r => r.UserId, _ => user.Id);
        }
    }
}
