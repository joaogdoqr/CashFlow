using CashFlow.Communication.Enums;
using CashFlow.Communication.Requests;
using CashFlow.Communication.Responses;
using CashFlow.Domain.Entities;
using CashFlow.Domain.Repositories;
using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Exception.ExceptionsBase;

namespace CashFlow.Application.UseCases.Expenses.Register
{
    public class RegisterExpenseUseCase(IExpensesRepository repository, IUnitOfWork unitOfWork) : IRegisterExpenseUseCase
    {
        private readonly IExpensesRepository _repository = repository;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public ResponseRegisterExpense Execute(RequestRegisterExpense request)
        {
            Validate(request);

           var entity =  new Expense()
            {
                Title = request.Title,
                Description = request.Description,
                Amount = request.Amount,
                Date = request.Date,
                PaymentType = (Domain.Enums.PaymentType)request.PaymentType,
            };

            _repository.Add(entity);

            _unitOfWork.Commit();

            return new ResponseRegisterExpense() { Title = request.Title };
        }

        private void Validate(RequestRegisterExpense request)
        {
            var validator = new RegisterExpenseValidator();

            var result = validator.Validate(request);

            var errorMessages = result.Errors.Select(f => f.ErrorMessage).ToList();

            if(result.IsValid is false)
            {
                throw new ErrorOnValidationException(errorMessages);
            }
        }
    }
}
