﻿namespace CashFlow.Communication.Responses.Expenses
{
    public class ResponseShortExpense
    {
        public long Id { get; set; }
        public required string Title { get; set; }
        public decimal Amount { get; set; }
    }
}
