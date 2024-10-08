using AutoMapper;
using CashFlow.Communication.Responses.Expense;
using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Domain.Services.LoggedUser;

namespace CashFlow.Application.UseCases.Expenses.GetAll
{
    public class GetAllExpensesUseCase(IExpensesReadOnlyRepository repository, ILoggedUser loggedUser, IMapper mapper) : IGetAllExpensesUseCase
    {
        private readonly IExpensesReadOnlyRepository _repository = repository;
        private readonly ILoggedUser _loggedUser = loggedUser;
        private readonly IMapper _mapper = mapper;

        public async Task<ResponseExpenses> Execute()
        {
            var loggedUser = await _loggedUser.Get();

            var result = await _repository.GetAll(loggedUser);

            return new ResponseExpenses
            {
                Expenses = _mapper.Map<List<ResponseShortExpense>>(result)
            };
        }
    }
}
