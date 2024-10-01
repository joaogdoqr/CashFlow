using CashFlow.Domain.Entities;
using CashFlow.Domain.Repositories.Users;
using Moq;

namespace CommonTestsUtilities.Repositories.Users
{
    public class UserReadOnlyRepositoryBuilder()
    {
        private readonly Mock<IUserReadOnlyRepository> _repository = new();

        public IUserReadOnlyRepository Build() => _repository.Object;

        public void ExistActiveUserWithEmail(string email)
        {
            _repository.Setup(userReadonlyRepository => userReadonlyRepository.ExistActiveUserWithEmail(email)).ReturnsAsync(true);
        }

        public UserReadOnlyRepositoryBuilder GetUserByEmail(User user)
        {
            _repository.Setup(userReadonlyRepository => userReadonlyRepository.GetUserByEmail(user.Email)).ReturnsAsync(user);

            return this; // returns the object itself after adding a setup
        }
    }
}
