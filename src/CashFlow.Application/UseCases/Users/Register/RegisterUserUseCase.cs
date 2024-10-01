using AutoMapper;
using CashFlow.Communication.Requests.Users;
using CashFlow.Communication.Responses.User;
using CashFlow.Domain.Entities;
using CashFlow.Domain.Repositories;
using CashFlow.Domain.Repositories.Users;
using CashFlow.Domain.Security.Cryptography;
using CashFlow.Domain.Security.Tokens;
using CashFlow.Exception;
using CashFlow.Exception.ExceptionsBase;
using FluentValidation.Results;

namespace CashFlow.Application.UseCases.Users.Register
{
    public class RegisterUserUseCase(IUserReadOnlyRepository userReadOnlyRepository, IUserWriteOnlyRepository userWriteOnlyRepository, IUnitOfWork unitOfWork,
        IMapper mapper, IPasswordEncrypter passwordEncrypter, IAccessTokenGenerator tokenGenerator) : IRegisterUserUseCase
    {
        private readonly IUserReadOnlyRepository _userReadOnlyRepository = userReadOnlyRepository;
        private readonly IUserWriteOnlyRepository _userWriteOnlyRepository = userWriteOnlyRepository;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;
        private readonly IPasswordEncrypter _passwordEncrypter = passwordEncrypter;
        private readonly IAccessTokenGenerator _tokenGenerator = tokenGenerator;

        public async Task<ResponseRegisteredUser> Execute(RequestRegisterUser request)
        {
            await Validate(request);

            var user = _mapper.Map<User>(request);
            user.Password = _passwordEncrypter.Encrypt(request.Password);
            user.UserIdentifier = Guid.NewGuid();

            await _userWriteOnlyRepository.Add(user);

            await _unitOfWork.Commit();

            return new ResponseRegisteredUser { Name = user.Name, Token = _tokenGenerator.Generate(user) };
        }

        public async Task Validate(RequestRegisterUser request)
        {
            var result = new RegisterUserValidator().Validate(request);

            var emailExist = await _userReadOnlyRepository.ExistActiveUserWithEmail(request.Email);
            if (emailExist)
            {
                result.Errors.Add(new ValidationFailure(string.Empty, ResourceErrorMessages.EMAIL_ALREADY_REGISTERED));
            }

            if(result.IsValid == false)
            {
                var errorMessages = result.Errors.Select(f=>f.ErrorMessage).ToList();

                throw new ErrorOnValidationException(errorMessages);
            }
        }
    }
}
