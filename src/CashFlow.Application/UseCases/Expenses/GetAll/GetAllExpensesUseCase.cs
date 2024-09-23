using AutoMapper;
using CashFlow.Communication.Responses.Expenses;
using CashFlow.Domain.Repositories.Expenses;

namespace CashFlow.Application.UseCases.Expenses.GetAll
{
    public class GetAllExpensesUseCase(IExpensesReadOnlyRepository repository, IMapper mapper) : IGetAllExpensesUseCase
    {
        private readonly IExpensesReadOnlyRepository _repository = repository;
        private readonly IMapper _mapper = mapper;

        public async Task<ResponseExpenses> Execute()
        {
            var result = await _repository.GetAll();

            return new ResponseExpenses
            {
                Expenses = _mapper.Map<List<ResponseShortExpense>>(result)
            };
        }
    }
}
