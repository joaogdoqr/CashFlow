namespace CashFlow.Infrastructure.Security.Tokens
{
    public interface ITokenProvider
    {
        string TokenOnRequest();
    }
}
