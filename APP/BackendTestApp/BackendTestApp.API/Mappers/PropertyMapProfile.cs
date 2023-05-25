using AutoMapper;
using BackendTestApp.API.Request;
using BackendTestApp.Contracts.Models;

namespace BackendTestApp.API.Mappers
{
    /// <summary>
    /// Class to configure the Mappers maps
    /// </summary>
    public class PropertyMapProfile : Profile
    {
        public PropertyMapProfile()
        {
            //Maps for EditPropertyRequest and OwnerDto
            CreateMap<EditPropertyRequest, OwnerDto>();

            //Maps for EditPropertyRequest and PropertyDto
            CreateMap<EditPropertyRequest, PropertyDto>().ForMember(p => p.PropertyOwner,
                                                                    map => map.MapFrom(s => s));
        }
    }
}
