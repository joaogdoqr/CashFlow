using AutoMapper;
using CashFlow.Communication.Responses.User;
using CashFlow.Domain.Services.LoggedUser;

namespace CashFlow.Application.UseCases.Users.Profile
{
    public class GetUserProfileUseCase(ILoggedUser loggedUser, IMapper mapper) : IGetUserProfileUseCase
    {
        private readonly ILoggedUser _loggedUser = loggedUser;
        private readonly IMapper _mapper = mapper;

        public async Task<ResponseUserProfile> Execute()
        {
            var user = await _loggedUser.Get();

            return _mapper.Map<ResponseUserProfile>(user);
        }
    }
}
