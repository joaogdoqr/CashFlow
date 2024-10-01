namespace CashFlow.Communication.Requests.Users
{
    public class RequestLogin
    {
        public required string Email { get; set; }
        public required string Password { get; set; }
    }
}
