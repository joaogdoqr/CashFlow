using CashFlow.Communication.Requests.Users;
using CashFlow.Domain.Entities;
using CashFlow.Domain.Repositories;
using CashFlow.Domain.Repositories.Users;
using CashFlow.Domain.Security.Cryptography;
using CashFlow.Domain.Services.LoggedUser;
using CashFlow.Exception;
using CashFlow.Exception.ExceptionsBase;
using FluentValidation.Results;

namespace CashFlow.Application.UseCases.Users.ChangePassoword
{
    public class ChangePasswordUseCase(ILoggedUser loggedUser, IUserUpdateOnlyRepository userUpdateOnlyRepository, IUnitOfWork unitOfWork,
        IPasswordEncrypter passwordEncrypter) : IChangePasswordUseCase
    {
        private readonly ILoggedUser _loggedUser = loggedUser;
        private readonly IPasswordEncrypter _passwordEncrypter = passwordEncrypter;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IUserUpdateOnlyRepository _userUpdateOnlyRepository = userUpdateOnlyRepository;

        public async Task Execute(RequestChangePassword request)
        {
            var loggedUser = await _loggedUser.Get();

            Validate(request, loggedUser);

            var user = await _userUpdateOnlyRepository.GetById(loggedUser.Id);
            user.Password = request.Password;

            _userUpdateOnlyRepository.Update(user);

            await _unitOfWork.Commit();
        }

        private void Validate(RequestChangePassword request, User loggedUser)
        {
            var validator = new ChangePasswordValidator();

            var result = validator.Validate(request);

            var passwordMatch = _passwordEncrypter.Verify(request.Password, loggedUser.Password);

            if (!passwordMatch)
                result.Errors.Add(new ValidationFailure(string.Empty, ResourceErrorMessages.INVALID_PASSWORD));


            if (!result.IsValid)
            {
                var erros = result.Errors.Select(e => e.ErrorMessage).ToList();
                throw new ErrorOnValidationException(erros);
            }
        }
    }
}
