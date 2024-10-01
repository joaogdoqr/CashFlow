using Bogus;
using CashFlow.Communication.Requests.Users;

namespace CommonTestsUtilities.Requests.Login
{
    public class RequestLoginBuilder
    {
        public static RequestLogin Build()
        {
            return new Faker<RequestLogin>()
            .RuleFor(user => user.Email, faker => faker.Internet.Email())
            .RuleFor(user => user.Password, faker => faker.Internet.Password(prefix: "Aa1!"));
        }
    }
}
