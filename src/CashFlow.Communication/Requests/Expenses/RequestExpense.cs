using CashFlow.Communication.Enums;

namespace CashFlow.Communication.Requests.Expenses
{
    public class RequestExpense
    {
        public required string Title { get; set; }
        public required string Description { get; set; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public PaymentType PaymentType { get; set; }
        public IList<Tags> Tags { get; set; } = [];
    }
}
