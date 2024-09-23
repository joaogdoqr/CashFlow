﻿using CashFlow.Application.Mappers;
using CashFlow.Application.UseCases.Expenses.Delete;
using CashFlow.Application.UseCases.Expenses.GetAll;
using CashFlow.Application.UseCases.Expenses.GetById;
using CashFlow.Application.UseCases.Expenses.Register;
using CashFlow.Application.UseCases.Expenses.Update;
using CashFlow.Application.UseCases.Login;
using CashFlow.Application.UseCases.Users.Register;
using Microsoft.Extensions.DependencyInjection;

namespace CashFlow.Api
{
    public static class DependencyInjectionExtension
    {
        public static void AddApplication(this IServiceCollection services)
        {
            AddUseCases(services);
            AddAutoMapper(services);
        }

        private static void AddUseCases(IServiceCollection services)
        {
            services.AddScoped<IRegisterExpenseUseCase, RegisterExpenseUseCase>();
            services.AddScoped<IGetAllExpensesUseCase, GetAllExpensesUseCase>();
            services.AddScoped<IGetByIdUseCase, GetByIdUseCase>();
            services.AddScoped<IDeleteExpenseUseCase, DeleteExpenseUseCase>();
            services.AddScoped<IUpdateExpenseUseCase, UpdateExpenseUseCase>();

            services.AddScoped<IRegisterUserUseCase, RegisterUserUseCase>();

            services.AddScoped<ILoginUseCase, LoginUseCase>();
        }

        private static void AddAutoMapper(IServiceCollection services)
        {
            services.AddAutoMapper(opt =>
            {
                opt.AddProfile<ExpenseMapper>();
            });
        }
    }
}
