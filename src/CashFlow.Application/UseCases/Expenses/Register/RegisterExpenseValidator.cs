using CashFlow.Communication.Requests;
using FluentValidation;

namespace CashFlow.Application.UseCases.Expenses.Register
{
    public class RegisterExpenseValidator : AbstractValidator<RequestRegisterExpense>
    {
        public RegisterExpenseValidator()
        {
            RuleFor(e => e.Title).NotEmpty().WithMessage("The title must not be empty");
            RuleFor(e => e.Amount).GreaterThan(0).WithMessage("The amount must be greater than zero");
            RuleFor(e => e.Date).LessThanOrEqualTo(DateTime.UtcNow).WithMessage("The date must be less than or equal to current date");
            RuleFor(e => e.PaymentType).IsInEnum().WithMessage("The payment type must be valid");
        }
    }
}
