using AutoMapper;
using BackendTestApp.API.Request;
using BackendTestApp.Contracts.Models;

namespace BackendTestApp.API.Mappers
{
    public class PropertyMapProfile : Profile
    {
        public PropertyMapProfile()
        {

            CreateMap<EditPropertyRequest, OwnerDto>();
            CreateMap<EditPropertyRequest, PropertyDto>().ForMember(p => p.PropertyOwner,
                                                                    map => map.MapFrom(s => s));

        }
    }
}
