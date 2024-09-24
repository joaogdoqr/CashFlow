using Bogus;
using CashFlow.Domain.Entities;
using CommonTestsUtilities.Cryptography;

namespace CommonTestsUtilities.Entities
{
    public class UserBuilder
    {
        public static User Build()
        {
            var passwordEncrypter = new PasswordEncrypterBuilder().Build();

            var user = new Faker<User>()
                .RuleFor(user => user.Id, _ => 1)
                .RuleFor(user => user.Name, faker => faker.Person.FirstName)
                .RuleFor(user => user.Email, faker => faker.Internet.Email())
                .RuleFor(user => user.Password, (_, user) => passwordEncrypter.Encrypt(user.Password))
                .RuleFor(user => user.UserIdentifier, _ => Guid.NewGuid());
            
            return user;
        }
    }
}
