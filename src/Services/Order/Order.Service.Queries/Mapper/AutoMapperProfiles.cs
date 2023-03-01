using AutoMapper;
using Order.Domain;
using Order.Service.Queries.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Service.Queries.Mapper
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<ClientOrder, ClientOrderDto>().ReverseMap();
        }
    }
}
