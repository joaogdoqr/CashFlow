using AutoMapper;
using CashFlow.Communication.Requests;
using CashFlow.Communication.Responses;
using CashFlow.Domain.Entities;

namespace CashFlow.Application.Mappers
{
    public class ExpenseMapper : Profile
    {
        public ExpenseMapper()
        {
            RequestToEntity();
            EntityToResponse();
        }

        private void RequestToEntity()
        {
            CreateMap<RequestExpense, Expense>();
        }

        private void EntityToResponse()
        {
            CreateMap<Expense, ResponseRegisterExpense>();
            CreateMap<Expense, ResponseShortExpense>();
            CreateMap<Expense, ResponseExpense>();
        }
    }
}
