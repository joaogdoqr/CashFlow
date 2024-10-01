using Bogus;
using CashFlow.Communication.Requests.Users;

namespace CommonTestsUtilities.Requests.Users
{
    public class RequestChangePasswordBuilder
    {
        public static RequestChangePassword Build()
        {
            return new Faker<RequestChangePassword>()
                .RuleFor(password => password.Password, faker => faker.Internet.Password(prefix: "Aa1!", length: 8))
                .RuleFor(password => password.NewPassword, faker => faker.Internet.Password(prefix: "Aa1!", length: 8));
        }
    }
}
