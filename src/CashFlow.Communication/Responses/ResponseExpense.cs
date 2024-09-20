using CashFlow.Communication.Enums;

namespace CashFlow.Communication.Responses
{
    public class ResponseExpense
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public PaymentType PaymentType { get; set; }
    }
}
