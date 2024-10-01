using CashFlow.Domain.Entities;
using CashFlow.Domain.Security.Tokens;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CashFlow.Infrastructure.Security.Tokens
{
    public class JwtTokenGenerator(uint expiresSeconds, string signingKey) : IAccessTokenGenerator
    {
        private readonly uint _expiresSeconds = expiresSeconds;
        private readonly string _signingKey = signingKey;
        private readonly SymmetricSecurityKey _symmetricSecurityKey = new(Encoding.UTF8.GetBytes(signingKey));
        private readonly string _securityAlgorithm = SecurityAlgorithms.HmacSha256Signature;

        public string Generate(User user)
        {
            var claims = new List<Claim>()
            {
                new(ClaimTypes.Name, user.Name),
                new(ClaimTypes.Sid, user.UserIdentifier.ToString()),
                new(ClaimTypes.Role, user.Role)
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Expires = DateTime.UtcNow.AddSeconds(_expiresSeconds),
                SigningCredentials = new SigningCredentials(_symmetricSecurityKey, _securityAlgorithm),
                Subject = new ClaimsIdentity(claims)
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var securityToken = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(securityToken);
        }
    }
}
