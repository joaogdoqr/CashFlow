using CashFlow.Domain.Entities;

namespace CashFlow.Domain.Repositories.Users
{
    public interface IUserUpdateOnlyRepository
    {
        Task<User> GetById(long id);
        void Update(User user);
    }
}
