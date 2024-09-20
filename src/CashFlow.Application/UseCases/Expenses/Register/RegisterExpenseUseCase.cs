using AutoMapper;
using CashFlow.Communication.Requests;
using CashFlow.Communication.Responses;
using CashFlow.Domain.Entities;
using CashFlow.Domain.Repositories;
using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Exception.ExceptionsBase;

namespace CashFlow.Application.UseCases.Expenses.Register
{
    public class RegisterExpenseUseCase(IExpensesWriteOnlyRepository repository, IUnitOfWork unitOfWork, IMapper mapper) : IRegisterExpenseUseCase
    {
        private readonly IExpensesWriteOnlyRepository _repository = repository;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        public async Task<ResponseRegisterExpense> Execute(RequestExpense request)
        {
            Validate(request);

            var entity = _mapper.Map<Expense>(request);

            await _repository.Add(entity);

            await _unitOfWork.Commit();

            return _mapper.Map<ResponseRegisterExpense>(entity);
        }

        private void Validate(RequestExpense request)
        {
            var validator = new ExpenseValidator();

            var result = validator.Validate(request);

            var errorMessages = result.Errors.Select(f => f.ErrorMessage).ToList();

            if(result.IsValid is false)
            {
                throw new ErrorOnValidationException(errorMessages);
            }
        }
    }
}
