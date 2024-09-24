using AutoMapper;
using CashFlow.Application.Mappers;

namespace CommonTestsUtilities.Mapper
{
    public class MapperBuilder
    {
        public static IMapper Build()
        {
            var mapper = new MapperConfiguration(config =>
            {
                config.AddProfile(new AutoMapping());
            });

            return mapper.CreateMapper();
        }
    }
}
