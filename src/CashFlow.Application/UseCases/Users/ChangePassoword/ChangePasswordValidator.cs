using CashFlow.Communication.Requests.Users;
using FluentValidation;

namespace CashFlow.Application.UseCases.Users.ChangePassoword
{
    public class ChangePasswordValidator : AbstractValidator<RequestChangePassword>
    {
        public ChangePasswordValidator()
        {
            RuleFor(password => password.NewPassword).SetValidator(new PasswordValidator<RequestChangePassword>());
        }
    }
}
