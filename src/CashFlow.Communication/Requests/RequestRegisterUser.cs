namespace CashFlow.Communication.Requests
{
    public class RequestRegisterUser
    {
        public required string Name { get; set; }

        public required string Email { get; set; }

        public required string Password { get; set; }
    }
}
