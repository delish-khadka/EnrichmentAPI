using AutoMapper;
using EnrichmentAPI.DTO;
using EnrichmentAPI.Models;

namespace EnrichmentAPI.Helper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Country, CountryDTO>().ReverseMap();
            CreateMap<Person, PersonDTO>().ReverseMap();
        }
    }
}
