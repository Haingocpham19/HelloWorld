using AutoMapper;
using Extension.Application.Dto;
using Extension.Domain.Entities;

namespace Extension.Web.Configs.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Currency, CurrencyDto>(); // Configure mapping from Source to Destination
            CreateMap<CurrencyDto, Currency>(); // Configure mapping from Destination to Source
        }
    }
}
