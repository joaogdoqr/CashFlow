
using CashFlow.Domain.Repositories;
using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Exception;
using CashFlow.Exception.ExceptionsBase;

namespace CashFlow.Application.UseCases.Expenses.Delete
{
    public class DeleteExpenseUseCase(IExpensesWriteOnlyRepository repository, IUnitOfWork unitOfWork) : IDeleteExpenseUseCase
    {
        private readonly IExpensesWriteOnlyRepository _repository = repository;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task Execute(long id)
        {
            var result = await _repository.Delete(id);

            if(result == false)
            {
                throw new NotFoundException(ResourceErrorMessages.EXPENSE_NOT_FOUND);
            }

            await _unitOfWork.Commit();
        }
    }
}
