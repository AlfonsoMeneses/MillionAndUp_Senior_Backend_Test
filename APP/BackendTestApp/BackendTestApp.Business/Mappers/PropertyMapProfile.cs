using AutoMapper;
using BackendTestApp.Contracts.Models;
using BackendTestApp.DataService.Entities;

namespace BackendTestApp.Business.Mappers
{  
    /// <summary>
    /// Class to configure the Mappers maps
    /// </summary>
    public class PropertyMapProfile : Profile
    {
        public PropertyMapProfile()
        {
            //Maps for PropertyDto and Property
            CreateMap<PropertyDto, Property>();
            CreateMap<Property, PropertyDto>();

            //Maps for OwnerDto and Owner
            CreateMap<OwnerDto, Owner>();
            CreateMap<Owner, OwnerDto>();

            //Maps for PropertyImageDto and PropertyImage
            CreateMap<PropertyImageDto, PropertyImage>();
            CreateMap<PropertyImage, PropertyImageDto>();
        }
    }
}

