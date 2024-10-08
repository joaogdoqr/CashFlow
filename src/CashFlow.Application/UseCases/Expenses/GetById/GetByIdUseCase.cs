using AutoMapper;
using CashFlow.Communication.Responses.Expense;
using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Domain.Services.LoggedUser;
using CashFlow.Exception;
using CashFlow.Exception.ExceptionsBase;

namespace CashFlow.Application.UseCases.Expenses.GetById
{
    public class GetByIdUseCase(IExpensesReadOnlyRepository repository, ILoggedUser loggedUser, IMapper mapper) : IGetByIdUseCase
    {
        private readonly IExpensesReadOnlyRepository _repository = repository;
        private readonly ILoggedUser _loggedUser = loggedUser;
        private readonly IMapper _mapper = mapper;

        public async Task<ResponseExpense> Execute(long id)
        {
            var loggedUser = await _loggedUser.Get();

            var expense = await _repository.GetById(id, loggedUser) ?? throw new NotFoundException(ResourceErrorMessages.EXPENSE_NOT_FOUND);

            return _mapper.Map<ResponseExpense>(expense);
        }
    }
}
