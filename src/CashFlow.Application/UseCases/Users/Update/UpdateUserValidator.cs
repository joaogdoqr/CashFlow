using CashFlow.Communication.Requests.Users;
using CashFlow.Exception;
using FluentValidation;

namespace CashFlow.Application.UseCases.Users.Update
{
    public class UpdateUserValidator : AbstractValidator<RequestUpdateUser>
    {
        public UpdateUserValidator()
        {
            RuleFor(user => user.Name)
                .NotEmpty()
                .WithMessage(ResourceErrorMessages.NAME_EMPTY);
            RuleFor(user => user.Email)
                .NotEmpty()
                .WithMessage(ResourceErrorMessages.EMAIL_EMPTY)
                .EmailAddress()
                .When(user => string.IsNullOrWhiteSpace(user.Email) == false, ApplyConditionTo.CurrentValidator)
                .WithMessage(ResourceErrorMessages.EMAIL_INVALID);
        }
    }
}
