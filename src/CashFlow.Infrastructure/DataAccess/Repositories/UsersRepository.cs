using CashFlow.Domain.Entities;
using CashFlow.Domain.Repositories.Users;
using CashFlow.Infrastructure.DataAccess.Contexts;
using Microsoft.EntityFrameworkCore;

namespace CashFlow.Infrastructure.DataAccess.Repositories
{
    internal class UsersRepository(CashFlowDbContext dbContext) : IUserReadOnlyRepository, IUserWriteOnlyRepository, IUserUpdateOnlyRepository
    {
        private readonly CashFlowDbContext _dbContext = dbContext;

        public async Task Add(User user)
        {
            await _dbContext.Users.AddAsync(user);
        }

        public async Task<bool> ExistActiveUserWithEmail(string email)
        {
             return await _dbContext.Users.AnyAsync(u=>u.Email.Equals(email));
        }

        public async Task<User> GetById(long id)
        {
            return await _dbContext.Users.FirstAsync(user => user.Id == id);
        }

        public async Task<User?> GetUserByEmail(string email)
        {
            return await _dbContext.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(user => user.Email.Equals(email));
        }

        public void Update(User user)
        {
            _dbContext.Users.Update(user);
        }
    }
}
