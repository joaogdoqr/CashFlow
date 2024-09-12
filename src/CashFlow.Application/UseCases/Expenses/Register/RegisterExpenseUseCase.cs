using CashFlow.Communication.Requests;
using CashFlow.Communication.Responses;
using CashFlow.Exception.ExceptionsBase;

namespace CashFlow.Application.UseCases.Expenses.Register
{
    public class RegisterExpenseUseCase
    {
        public ResponseRegisterExpense Execute(RequestRegisterExpense request)
        {
            Validate(request);

            return new ResponseRegisterExpense()
            {
                Title = request.Title
            };
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
