using AutoMapper;
using BackendTestApp.API.Controllers;
using BackendTestApp.API.Request;
using BackendTestApp.Business.Services;
using BackendTestApp.Contracts.Models;
using BackendTestApp.DataService;
using BackendTestApp.DataService.Entities;
using Microsoft.EntityFrameworkCore;

namespace BackendTestApp.UnitTest.Config
{
    /// <summary>
    /// Configuration From The Unit Test
    /// </summary>
    public class UnitTestConfig
    {
        /// <summary>
        /// Get The Mapper Of The API Controller 
        /// </summary>
        private static IMapper GetControllerMapper
        {
            get
            {
                //Set the Mapper Configuration
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<EditPropertyRequest, OwnerDto>();
                    cfg.CreateMap<EditPropertyRequest, PropertyDto>().ForMember(p => p.PropertyOwner, map => map.MapFrom(s => s));
                });

                //Return The Mapper
                return new Mapper(config);
            }
        }

        /// <summary>
        /// Get The Mapper Of The Business Service
        /// </summary>
        private static IMapper GetServiceMapper
        {
            get
            {
                //Set the Mapper Configuration
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<PropertyDto, Property>();
                    cfg.CreateMap<Property, PropertyDto>();

                    cfg.CreateMap<OwnerDto, Owner>();
                    cfg.CreateMap<Owner, OwnerDto>();

                    cfg.CreateMap<PropertyImageDto, PropertyImage>();
                    cfg.CreateMap<PropertyImage, PropertyImageDto>();
                });

                //Return The Mapper
                return new Mapper(config);
            }
        }

        /// <summary>
        /// Get the DB Context
        /// </summary>
        private static DbContext GetDbService
        {
            get
            {
                //Set the Backend DB Context
                return new BackendTestDB();
            }
        }

        /// <summary>
        /// Get The API Property Controller 
        /// </summary>
        /// <returns>API Property Controller</returns>
        public static PropertyController GetPropertyController()
        {
            //Create an instance of the Service
            var service = new PropertyService((BackendTestDB)GetDbService, GetServiceMapper);

            //Create and send an instance of the Property Controller 
            return new PropertyController(service, GetControllerMapper);
        }
    }
}
