using System.Net;

namespace CashFlow.Exception.ExceptionsBase
{
    public class NotFoundException(string message) : CashFlowException(message)
    {
        public override int StatusCode => HttpStatusCode.NotFound.GetHashCode();

        public override List<string> GetErros() => [Message];
    }
}
