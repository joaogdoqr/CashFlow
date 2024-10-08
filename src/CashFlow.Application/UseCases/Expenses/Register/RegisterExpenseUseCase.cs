using AutoMapper;
using CashFlow.Communication.Requests.Expenses;
using CashFlow.Communication.Responses.Expense;
using CashFlow.Domain.Entities;
using CashFlow.Domain.Repositories;
using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Domain.Services.LoggedUser;
using CashFlow.Exception.ExceptionsBase;

namespace CashFlow.Application.UseCases.Expenses.Register
{
    public class RegisterExpenseUseCase(IExpensesWriteOnlyRepository repository, ILoggedUser loggedUser,
        IUnitOfWork unitOfWork, IMapper mapper) : IRegisterExpenseUseCase
    {
        private readonly IExpensesWriteOnlyRepository _repository = repository;
        private readonly ILoggedUser _loggedUser = loggedUser;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        public async Task<ResponseRegisterExpense> Execute(RequestExpense request)
        {
            Validate(request);

            var loggedUser = await _loggedUser.Get();

            var expense = _mapper.Map<Expense>(request);
            expense.UserId = loggedUser.Id;

            await _repository.Add(expense);

            await _unitOfWork.Commit();

            return _mapper.Map<ResponseRegisterExpense>(expense);
        }

        private static void Validate(RequestExpense request)
        {
            var validator = new ExpenseValidator();

            var result = validator.Validate(request);

            var errorMessages = result.Errors.Select(f => f.ErrorMessage).ToList();

            if(result.IsValid is false)
                throw new ErrorOnValidationException(errorMessages);
        }
    }
}
