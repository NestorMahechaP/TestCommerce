using AutoMapper;
using Catalog.Domain;
using Catalog.Service.Queries.DTOs;

namespace Catalog.Service.Queries.Mapper
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Product, ProductDto>().ReverseMap();
        }
    }
}
