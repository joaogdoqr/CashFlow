using CashFlow.Communication.Requests.Users;

namespace CashFlow.Application.UseCases.Users.ChangePassoword
{
    public interface IChangePasswordUseCase
    {
        Task Execute(RequestChangePassword request);
    }
}
