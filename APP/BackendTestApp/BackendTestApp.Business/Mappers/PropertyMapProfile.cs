using AutoMapper;
using BackendTestApp.Contracts.Models;
using BackendTestApp.DataService.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendTestApp.Business.Mappers
{
    public class PropertyMapProfile : Profile
    {
        public PropertyMapProfile()
        {
            CreateMap<PropertyDto, Property>();
            CreateMap<Property, PropertyDto>();

            CreateMap<OwnerDto, Owner>();
            CreateMap<Owner, OwnerDto>();

            CreateMap<PropertyImageDto, PropertyImage>();
            CreateMap<PropertyImage, PropertyImageDto>();
        }
    }
}

