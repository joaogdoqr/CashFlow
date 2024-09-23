namespace CashFlow.Communication.Responses.User
{
    public class ResponseRegisteredUser
    {
        public required string Name { get;set; }

        public string Token { get; set; } = string.Empty;
    }
}
