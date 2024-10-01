using CashFlow.Communication.Requests.Users;
using CashFlow.Communication.Responses.User;

namespace CashFlow.Application.UseCases.Users.Register
{
    public interface IRegisterUserUseCase
    {
        Task<ResponseRegisteredUser> Execute(RequestRegisterUser request);
    }
}
