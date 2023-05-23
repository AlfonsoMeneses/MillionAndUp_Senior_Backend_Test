using AutoMapper;
using BackendTestApp.API.Request;
using BackendTestApp.Contracts.Models;

namespace BackendTestApp.API.Mappers
{
    public class PropertyMapProfile: Profile
    {
        public PropertyMapProfile()
        {
            
            CreateMap<EditPropertyRequest, OwnerDto>();
            CreateMap<EditPropertyRequest, PropertyDto>()//.ForMember(p => p.Name, map=> map.MapFrom(e => e.Name))
                                                         //.ForMember(p => p.Price, map => map.MapFrom(e => e.Price))
                                                         //.ForMember(p => p.Price, map => map.MapFrom(e => e.Price))
                                                         .ForMember(p => p.PropertyOwner, 
                                                                    map => map.MapFrom(s =>s)); 
            
        }
    }
}
