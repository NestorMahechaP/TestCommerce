using AutoMapper;
using Customer.Domain;
using Customer.Service.Queries.DTOs;

namespace Customer.Service.Queries.Mapper
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Client, ClientDto>().ReverseMap();
        }
    }
}
