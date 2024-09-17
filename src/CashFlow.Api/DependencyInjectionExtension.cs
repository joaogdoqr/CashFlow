using CashFlow.Application.UseCases.Expenses.Register;

namespace CashFlow.Api
{
    public static class DependencyInjectionExtension
    {
        public static void AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IRegisterExpenseUseCase, RegisterExpenseUseCase>();
        }

    }
}
