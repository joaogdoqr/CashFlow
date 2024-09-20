using System.Net;

namespace CashFlow.Exception.ExceptionsBase
{
    public class ErrorOnValidationException(List<string> errorMessages) : CashFlowException(string.Empty)
    {
        private readonly List<string> _errors = errorMessages;

        public override int StatusCode => HttpStatusCode.BadRequest.GetHashCode();

        public override List<string> GetErros() => _errors;
    }
}
