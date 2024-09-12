using CashFlow.Communication.Responses;

namespace CashFlow.Communication.Responses
{
    public class ResponseError
    {
        public List<string> ErrorMessage { get; set; }

        public ResponseError(string errorMessage)
        {
            ErrorMessage = [errorMessage];
        }

        public ResponseError(List<string> errorMessage)
        {
            ErrorMessage = errorMessage;
        }
    }
}