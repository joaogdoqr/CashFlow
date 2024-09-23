using CashFlow.Communication.Requests;
using CashFlow.Communication.Responses.User;

namespace CashFlow.Application.UseCases.Users.Register
{
    public interface IRegisterUserUseCase
    {
        Task<ResponseRegisteredUser> Execute(RequestRegisterUser request);
    }
}
