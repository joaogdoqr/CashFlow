
using CashFlow.Domain.Repositories;
using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Domain.Services.LoggedUser;
using CashFlow.Exception;
using CashFlow.Exception.ExceptionsBase;

namespace CashFlow.Application.UseCases.Expenses.Delete
{
    public class DeleteExpenseUseCase(IExpensesWriteOnlyRepository _writeOnlyRepository, IExpensesReadOnlyRepository _readOnlyRepository,
        ILoggedUser loggedUser, IUnitOfWork unitOfWork) : IDeleteExpenseUseCase
    {
        private readonly IExpensesWriteOnlyRepository _writeOnlyRepository = _writeOnlyRepository;
        private readonly IExpensesReadOnlyRepository _readOnlyRepository = _readOnlyRepository;
        private readonly ILoggedUser _loggedUser = loggedUser;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task Execute(long id)
        {
            var loggedUser = await _loggedUser.Get();

            var expense = await _readOnlyRepository.GetById(id, loggedUser) ?? throw new NotFoundException(ResourceErrorMessages.EXPENSE_NOT_FOUND);
            
            await _writeOnlyRepository.Delete(expense.Id);

            await _unitOfWork.Commit();
        }
    }
}
