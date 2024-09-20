namespace CashFlow.Exception.ExceptionsBase
{
    public abstract class CashFlowException(string message) : SystemException(message)
    {
        public abstract int StatusCode { get; }

        public abstract List<string> GetErros();
    }
}
