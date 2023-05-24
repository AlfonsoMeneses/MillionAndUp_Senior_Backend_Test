using AutoMapper;
using BackendTestApp.Business.Helpers;
using BackendTestApp.Contracts.Exceptions;
using BackendTestApp.Contracts.Models;
using BackendTestApp.Contracts.Services;
using BackendTestApp.DataService;
using BackendTestApp.DataService.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BackendTestApp.Business.Services
{
    public class PropertyService : IPropertyService
    {
        private readonly BackendTestDB _db;
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
        public PropertyDto Create(string name, string address, decimal price, int year, int? idOwner)
        {
            //Validations 

            if (name == null || name.Trim().IsNullOrEmpty())
            {
                throw new PropertyException("Name Is Required");
            }

            var nProperties = _db.Properties.Where(p => p.Name.ToUpper().Trim().Equals(name.ToUpper().Trim())).Count();

            if (nProperties > 0)
            {
                throw new PropertyException("There is another property with that name");
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

            if (idOwner != null)
            {
                var owner = _db.Owners.FirstOrDefault(o => o.IdOwner == idOwner.Value);

                if (owner == null)
                {
                    throw new PropertyException("Invalid Owner");
                }
                else
                {
                    newProperty.PropertyOwner = owner;
                }
            }


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

            var lst = _db.Properties.Include("PropertyOwner")
                                    .Include("PropertyImages")
                                    .Where(p => p.Name.Contains(filter.Name != null ? filter.Name : string.Empty) &&
                                              p.Address.Contains(filter.Address != null ? filter.Address : string.Empty) &&
                                              p.Price >= filter.MinPrice &&
                                              (p.Price <= filter.MaxPrice || filter.MaxPrice == 0) &&
                                              p.Year >= filter.FromYear &&
                                              (p.Year <= filter.ToYear || filter.ToYear == 0) &&
                                              (p.IdOwner == filter.IdOwner || filter.IdOwner == 0))
                                    .ToList();

            foreach (var item in lst)
            {
                properties.Add(_mapper.Map<PropertyDto>(item));
            }

            return properties;
        }

        /// <summary>
        /// Get Property By ID
        /// </summary>
        /// <param name="idProperty"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public PropertyDto GetPropertyById(int idProperty)
        {
            var property = GetProperty(idProperty);
            return _mapper.Map<PropertyDto>(property);
        }

        /// <summary>
        /// Change Property Price
        /// </summary>
        /// <param name="idProperty"></param>
        /// <param name="price"></param>
        /// <returns></returns>
        /// <exception cref="PropertyException"></exception>
        public PropertyDto ChangePrice(int idProperty, decimal price)
        {
            if (price <= 0)
            {
                throw new PropertyException("Invalid Price");
            }

            var property = GetProperty(idProperty);

            property.Price = price;

            _db.Properties.Update(property);
            _db.SaveChanges();

            return _mapper.Map<PropertyDto>(property);
        }

        /// <summary>
        /// Update Property
        /// </summary>
        /// <param name="idProperty"></param>
        /// <param name="property"></param>
        /// <returns></returns>
        public PropertyDto Update(int idProperty, PropertyDto property)
        {
            if (property == null)
            {
                throw new PropertyException("Invalid Property Data To Update");
            }

            var propertyToUpdate = GetProperty(idProperty);

            if (!String.IsNullOrEmpty(property.Name) && !property.Name.Equals(propertyToUpdate.Name))
            {
                propertyToUpdate.Name = property.Name;
            }

            if (!String.IsNullOrEmpty(property.Address) && !property.Address.Equals(propertyToUpdate.Address))
            {
                propertyToUpdate.Address = property.Address;
            }

            if (property.Year >= 1900 && property.Year != propertyToUpdate.Year)
            {
                propertyToUpdate.Year = property.Year;
            }

            if (property.PropertyOwner != null && property.PropertyOwner.IdOwner >0 && property.PropertyOwner.IdOwner != propertyToUpdate.IdOwner)
            {
                var owner = _db.Owners.FirstOrDefault(o => o.IdOwner == property.PropertyOwner.IdOwner);

                if (owner == null)
                {
                    throw new PropertyException("Invalid Owner");
                }
                else
                {
                    propertyToUpdate.PropertyOwner = owner;
                }
            }

            _db.Properties.Update(propertyToUpdate);

            _db.SaveChanges();

            return _mapper.Map<PropertyDto>(propertyToUpdate);
        }

        /// <summary>
        /// Add Image To A Property
        /// </summary>
        /// <param name="idProperty"></param>
        /// <param name="image"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public PropertyImageDto AddImage(int idProperty, byte[] image)
        {

            if (image == null || image.Length < 1)
            {
                throw new PropertyException("Invalid Image");
            }

            var propety = GetProperty(idProperty);

            var imageName = ImageHelper.AddImage(propety.CodeInternal, image);

            var propertyImage = new PropertyImage
            {
                Property = propety,
                Image = imageName
            };

            _db.PropertyImages.Add(propertyImage);
            _db.SaveChanges();

            return _mapper.Map<PropertyImageDto>(propertyImage);

        }


        /// <summary>
        /// Get Image
        /// </summary>
        /// <param name="idProperty"></param>
        /// <param name="idPropertyImage"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public byte[] GetImage(int idProperty, int idPropertyImage)
        {
            var property = GetProperty(idProperty);

            var propertyImage = _db.PropertyImages.FirstOrDefault(im => im.IdProperty == idProperty && im.IdPropertyImage == idPropertyImage);

            if (propertyImage == null)
            {
                throw new PropertyException("Invalid Image");
            }

            return ImageHelper.GetImage(property.CodeInternal, propertyImage.Image);
        }

        #region
        /// <summary>
        /// Get Property By ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="PropertyException"></exception>
        private Property GetProperty(int id)
        {
            var property = _db.Properties.Include("PropertyOwner")
                                         .Include("PropertyImages")
                                         .FirstOrDefault(p => p.IdProperty == id);

            if (property == null)
            {
                throw new PropertyException("Invalid Property");
            }

            return property;
        }


        #endregion
    }
}
