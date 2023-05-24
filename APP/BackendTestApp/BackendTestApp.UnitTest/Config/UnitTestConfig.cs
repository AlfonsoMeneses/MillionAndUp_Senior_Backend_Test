using AutoMapper;
using BackendTestApp.API.Controllers;
using BackendTestApp.API.Request;
using BackendTestApp.Business.Services;
using BackendTestApp.Contracts.Models;
using BackendTestApp.DataService;
using BackendTestApp.DataService.Entities;

namespace BackendTestApp.UnitTest.Config
{
    public class UnitTestConfig
    {
        private static IMapper GetControllerMapper
        {
            get
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<EditPropertyRequest, OwnerDto>();
                    cfg.CreateMap<EditPropertyRequest, PropertyDto>().ForMember(p => p.PropertyOwner, map => map.MapFrom(s => s));
                });

                return new Mapper(config);
            }
        }

        private static IMapper GetServiceMapper
        {
            get
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<PropertyDto, Property>();
                    cfg.CreateMap<Property, PropertyDto>();

                    cfg.CreateMap<OwnerDto, Owner>();
                    cfg.CreateMap<Owner, OwnerDto>();

                    cfg.CreateMap<PropertyImageDto, PropertyImage>();
                    cfg.CreateMap<PropertyImage, PropertyImageDto>();
                });

                return new Mapper(config);
            }
        }

        private static BackendTestDB GetDbService
        {
            get
            {
                return new BackendTestDB();
            }
        }

        public static PropertyController GetPropertyController()
        {
            var service = new PropertyService(GetDbService, GetServiceMapper);

            return new PropertyController(service, GetControllerMapper);
        }
    }
}
