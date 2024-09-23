using CashFlow.Communication.Requests;
using CashFlow.Communication.Responses.User;

namespace CashFlow.Application.UseCases.Login
{
    public interface ILoginUseCase
    {
        Task<ResponseRegisteredUser> Execute(RequestLogin request);
    }
}
