using CashFlow.Infrastructure.Security.Tokens;

namespace CashFlow.Api.Token
{
    public class HttpContextTokenValue(IHttpContextAccessor contextAccessor) : ITokenProvider
    {
        private IHttpContextAccessor _contextAccessor = contextAccessor;

        public string TokenOnRequest()
        {
            var authorization = _contextAccessor.HttpContext!.Request.Headers.Authorization.ToString();

            return authorization["Bearer".Length..].Trim();
        }
    }
}
