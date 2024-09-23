using System.Net;

namespace CashFlow.Exception.ExceptionsBase
{
    public class InvalidLoginException() : CashFlowException(ResourceErrorMessages.EMAIL_OR_PASSWORD_INVALID)
    {
        public override int StatusCode => HttpStatusCode.Unauthorized.GetHashCode();

        public override List<string> GetErros()
        {
            return [Message];
        }
    }
}
