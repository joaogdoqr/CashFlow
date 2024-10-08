using AutoMapper;
using CashFlow.Communication.Requests.Expenses;
using CashFlow.Communication.Requests.Users;
using CashFlow.Communication.Responses.Expense;
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
            CreateMap<RequestExpense, Expense>()
                .ForMember(dest => dest.Tags, opt => opt.MapFrom(source => source.Tags.Distinct()));

            CreateMap<Communication.Enums.Tags, Tag>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src));

            CreateMap<RequestRegisterUser, User>()
                .ForMember(dest=>dest.Password, opt=>opt.Ignore());

            CreateMap<RequestUpdateUser, User>();
        }

        private void EntityToResponse()
        {
            CreateMap<Expense, ResponseRegisterExpense>();
            CreateMap<Expense, ResponseShortExpense>();
            CreateMap<Expense, ResponseExpense>()
                .ForMember(dest => dest.Tags, opt => opt.MapFrom(src => src.Tags.Select(t => t.Name)));

            CreateMap<User, ResponseUserProfile>();
        }
    }
}
