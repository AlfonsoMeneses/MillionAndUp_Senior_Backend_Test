using AutoMapper;
using BackendTestApp.Contracts.Exceptions;
using BackendTestApp.Contracts.Models;
using BackendTestApp.Contracts.Services;
using BackendTestApp.DataService;
using BackendTestApp.DataService.Entities;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendTestApp.Business.Services
{
    public class PropertyService : IPropertyService
    {
        private BackendTestDB _db;
        private readonly IMapper _mapper;

        public PropertyService(BackendTestDB db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        /// <summary>
        /// Create a new propery
        /// </summary>
        /// <param name="name"></param>
        /// <param name="address"></param>
        /// <param name="price"></param>
        /// <param name="year"></param>
        /// <returns></returns>
        /// <exception cref="PropertyException"></exception>
        public PropertyDto Create(string name, string address, decimal price, int year)
        {
            //Validations 

            if (name.Trim().IsNullOrEmpty())
            {
                throw new PropertyException("Name Is Required");
            }

            if (address.Trim().IsNullOrEmpty())
            {
                throw new PropertyException("Adrress Is Required");
            }

            if (price <= 0)
            {
                throw new PropertyException("Invalid Price");
            }

            if (year < 1900)
            {
                throw new PropertyException("Invalid Year");
            }

            //Setting data

            var newProperty = new Property
            {
                Name = name,
                Address = address,
                Price = price,
                Year = (short)year,
                CodeInternal = "P" + DateTime.Now.Ticks
            };

            //Saving 
            _db.Properties.Add(newProperty);

            _db.SaveChanges();

            return _mapper.Map<PropertyDto>(newProperty);
        }

        /// <summary>
        /// Get List of Properties
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public IList<PropertyDto> GetProperties(PropertyFilter filter)
        {
            var properties = new List<PropertyDto>();

            var lst = _db.Properties.Where(p=>p.Name.Contains(filter.Name != null ? filter.Name: string.Empty) &&
                                              p.Address.Contains(filter.Address!= null ? filter.Address: string.Empty) &&
                                              p.Price >= filter.MinPrice &&
                                              (p.Price <= filter.MaxPrice || filter.MaxPrice == 0) &&
                                              p.Year >=filter.FromYear &&
                                              (p.Year <= filter.ToYear || filter.ToYear == 0) &&
                                              (p.IdOwner == filter.IdOwner || filter.IdOwner == 0))
                                    .ToList();

            foreach (var item in lst)
            {
                properties.Add(_mapper.Map<PropertyDto>(item));
            }

            return properties;
        }

        public PropertyDto ChangePrice(int idProperty, decimal price)
        {
            if (price <= 0)
            {
                throw new PropertyException("Invalid Price");
            }

            var property = _db.Properties.FirstOrDefault(p => p.IdProperty == idProperty);

            if (property == null)
            {
                throw new PropertyException("Invalid Property");
            }

            property.Price = price;

            _db.Properties.Update(property);
            _db.SaveChanges();

            return _mapper.Map<PropertyDto>(property);
        }
    }
}
