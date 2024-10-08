using CashFlow.Communication.Enums;

namespace CashFlow.Communication.Responses.Expense
{
    public class ResponseExpense
    {
        public long Id { get; set; }
        public required string Title { get; set; }
        public string? Description { get; set; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public PaymentType PaymentType { get; set; }
        public IList<Tags> Tags { get; set; } = [];
    }
}
