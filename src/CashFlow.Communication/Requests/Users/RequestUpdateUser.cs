namespace CashFlow.Communication.Requests.Users
{
    public class RequestUpdateUser
    {
        public required string Name { get; set; }
        public required string Email { get; set; }
    }
}
