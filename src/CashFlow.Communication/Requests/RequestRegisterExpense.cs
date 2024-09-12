﻿using CashFlow.Communication.Enums;

namespace CashFlow.Communication.Requests
{
    public class RequestRegisterExpense
    {
        public required string Title { get; set; }
        public required string Description { get; set; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public PaymentType PaymentType { get; set; }
    }
}
