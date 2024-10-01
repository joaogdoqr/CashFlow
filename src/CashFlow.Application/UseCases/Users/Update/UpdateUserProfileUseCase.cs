using AutoMapper;
using CashFlow.Communication.Requests.Users;
using CashFlow.Domain.Repositories;
using CashFlow.Domain.Repositories.Users;
using CashFlow.Domain.Services.LoggedUser;
using CashFlow.Exception;
using CashFlow.Exception.ExceptionsBase;
using FluentValidation.Results;

namespace CashFlow.Application.UseCases.Users.Update
{
    public class UpdateUserProfileUseCase(ILoggedUser loggedUser, IUserUpdateOnlyRepository userUpdateOnlyRepository, 
        IUserReadOnlyRepository userReadOnlyRepository, IUnitOfWork unitOfWork, IMapper mapper) : IUpdateUserProfileUseCase
    {
        private readonly ILoggedUser _loggedUser = loggedUser;
        private readonly IUserUpdateOnlyRepository _userUpdateOnlyRepository = userUpdateOnlyRepository;
        private readonly IUserReadOnlyRepository _userReadOnlyRepository = userReadOnlyRepository;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        public async Task Execute(RequestUpdateUser request)
        {
            var loggedUser = await _loggedUser.Get();

            await Validate(request, loggedUser.Email);

            var user = await _userUpdateOnlyRepository.GetById(loggedUser.Id);

            var newUser = _mapper.Map(request, user);

            _userUpdateOnlyRepository.Update(newUser);

            await _unitOfWork.Commit();
        }

        private async Task Validate(RequestUpdateUser request, string currentEmail)
        {
            var validator = new UpdateUserValidator();

            var result = validator.Validate(request);

            if(currentEmail.Equals(request.Email)== false)
            {
                var userExist = await _userReadOnlyRepository.ExistActiveUserWithEmail(request.Email);

                if(userExist)
                        result.Errors.Add(new ValidationFailure(string.Empty, ResourceErrorMessages.EMAIL_ALREADY_REGISTERED));
            }

            if (!result.IsValid)
            {
                var errorMessages = result.Errors.Select(error => error.ErrorMessage).ToList();

                throw new ErrorOnValidationException(errorMessages);
            }
        }
    }
}
