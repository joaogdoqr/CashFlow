namespace CashFlow.Communication.Requests.Users
{
    public class RequestChangePassword
    {
        public required string Password { get; set; }

        public required string NewPassword { get; set; }
    }
}
