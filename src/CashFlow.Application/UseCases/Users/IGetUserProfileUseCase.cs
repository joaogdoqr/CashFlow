using CashFlow.Communication.Responses.User;

namespace CashFlow.Application.UseCases.Users
{
    public interface IGetUserProfileUseCase
    {
        Task<ResponseUserProfile> Execute();
    }
}
