using Bogus;
using CashFlow.Communication.Enums;
using CashFlow.Communication.Requests;

namespace CommonTestsUtilities.Requests;

public class RequestRegisterExpenseBuilder
{
    public static RequestExpense Build()
    {
        return new Faker<RequestExpense>()
            .RuleFor(r => r.Title, f => f.Commerce.ProductName())
            .RuleFor(r => r.Description, f => f.Commerce.ProductDescription())
            .RuleFor(r => r.Date, f => f.Date.Past())
            .RuleFor(r => r.Amount, f => f.Random.Decimal(min: 1, max: 1000))
            .RuleFor(r => r.PaymentType, f => f.PickRandom<PaymentType>());
    }
}