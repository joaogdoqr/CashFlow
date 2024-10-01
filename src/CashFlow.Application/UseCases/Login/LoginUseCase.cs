using CashFlow.Communication.Requests.Users;
using CashFlow.Communication.Responses.User;
using CashFlow.Domain.Repositories.Users;
using CashFlow.Domain.Security.Cryptography;
using CashFlow.Domain.Security.Tokens;
using CashFlow.Exception.ExceptionsBase;

namespace CashFlow.Application.UseCases.Login
{
    public class LoginUseCase(IUserReadOnlyRepository repository, IPasswordEncrypter passwordEncrypter, IAccessTokenGenerator accessTokenGenerator) : ILoginUseCase
    {
        private readonly IUserReadOnlyRepository _repository = repository;
        private readonly IPasswordEncrypter _passwordEncrypter = passwordEncrypter;
        private readonly IAccessTokenGenerator _accessTokenGenerator = accessTokenGenerator;

        public async Task<ResponseRegisteredUser> Execute(RequestLogin request)
        {
            var user = await _repository.GetUserByEmail(request.Email) ?? throw new InvalidLoginException();

            var isPasswordValid = _passwordEncrypter.Verify(request.Password, user.Password);

            if (!isPasswordValid)
            {
                throw new InvalidLoginException();
            }

            return new ResponseRegisteredUser { Name = user.Name, Token = _accessTokenGenerator.Generate(user) };
        }
    }
}
