using CashFlow.Domain.Entities;
using CashFlow.Domain.Services.LoggedUser;
using CashFlow.Infrastructure.DataAccess.Contexts;
using CashFlow.Infrastructure.Security.Tokens;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace CashFlow.Infrastructure.Services.LoggedUser
{
    public class LoggedUser(CashFlowDbContext dbContext, ITokenProvider tokenProvider) : ILoggedUser
    {
        private readonly CashFlowDbContext _dbContext = dbContext;
        private readonly ITokenProvider _tokenProvider = tokenProvider;

        public async Task<User> Get()
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var jwtSecurityToken = tokenHandler.ReadJwtToken(_tokenProvider.TokenOnRequest());

            var guid = jwtSecurityToken.Claims.First(claim => claim.Type == ClaimTypes.Sid).Value;

            return await _dbContext.Users
                .AsNoTracking()
                .FirstAsync(user => user.UserIdentifier == Guid.Parse(guid));
        }
    }
}
