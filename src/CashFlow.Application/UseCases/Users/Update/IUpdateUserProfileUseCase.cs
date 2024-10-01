using CashFlow.Communication.Requests.Users;

namespace CashFlow.Application.UseCases.Users.Update
{
    public interface IUpdateUserProfileUseCase
    {
        Task Execute(RequestUpdateUser request);
    }
}
