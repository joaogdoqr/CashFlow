using AutoMapper;
using CashFlow.Communication.Requests.Expenses;
using CashFlow.Communication.Requests.Users;
using CashFlow.Communication.Responses.Expenses;
using CashFlow.Communication.Responses.User;
using CashFlow.Domain.Entities;

namespace CashFlow.Application.Mappers
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            RequestToEntity();
            EntityToResponse();
        }

        private void RequestToEntity()
        {
            CreateMap<RequestExpense, Expense>();

            CreateMap<RequestRegisterUser, User>()
                .ForMember(dest=>dest.Password, opt=>opt.Ignore());

            CreateMap<RequestUpdateUser, User>();
        }

        private void EntityToResponse()
        {
            CreateMap<Expense, ResponseRegisterExpense>();
            CreateMap<Expense, ResponseShortExpense>();
            CreateMap<Expense, ResponseExpense>();

            CreateMap<User, ResponseUserProfile>();
        }
    }
}
