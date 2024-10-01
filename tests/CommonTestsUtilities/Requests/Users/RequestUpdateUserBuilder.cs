using Bogus;
using CashFlow.Communication.Requests.Users;

namespace CommonTestsUtilities.Requests.Users
{
    public class RequestUpdateUserBuilder
    {
        public static RequestUpdateUser Build()
        {
            return new Faker<RequestUpdateUser>()
                .RuleFor(user => user.Name, faker => faker.Person.FirstName)
                .RuleFor(user => user.Email, (faker, user) => faker.Internet.Email(user.Name));
        }
    }
}
