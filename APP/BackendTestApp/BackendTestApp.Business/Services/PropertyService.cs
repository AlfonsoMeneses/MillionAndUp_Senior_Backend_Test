using AutoMapper;
using BackendTestApp.Business.Helpers;
using BackendTestApp.Contracts.Exceptions;
using BackendTestApp.Contracts.Models;
using BackendTestApp.Contracts.Services;
using BackendTestApp.DataService;
using BackendTestApp.DataService.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace BackendTestApp.Business.Services
{
    /// <summary>
    /// Property Service
    /// </summary>
    public class PropertyService : IPropertyService
    {
        /// <summary>
        /// DB Service
        /// </summary>
        private readonly BackendTestDB _db;

        /// <summary>
        /// Mapper Service
        /// </summary>
        private readonly IMapper _mapper;

        public PropertyService(BackendTestDB db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        /// <summary>
        /// Create a new property
        /// </summary>
        /// <param name="name">Property Name</param>
        /// <param name="address">Property Address</param>
        /// <param name="price">Property Price</param>
        /// <param name="year">Property Taxable Year</param>
        /// <param name="idOwner">Property Owner</param>
        /// <returns>Return the new property created</returns>
        /// <exception cref="PropertyException"></exception>
        public PropertyDto Create(string name, string address, decimal price, int year, int? idOwner)
        {
            //Filter Validations

            //Name Validation
            if (name == null || name.Trim().IsNullOrEmpty())
            {
                throw new PropertyException("Name Is Required");
            }

            //Getting a property with the name of the new property to create
            var oldPropertie = _db.Properties.FirstOrDefault(p => p.Name.ToUpper().Trim().Equals(name.ToUpper().Trim()));

            //If there's a property with this name
            if (oldPropertie != null)
            {
                //Exception
                throw new PropertyException("There is another property with that name");
            }

            //Address Validation
            if (address.Trim().IsNullOrEmpty())
            {
                throw new PropertyException("Adrress Is Required");
            }

            //Price Validation
            if (price <= 0)
            {
                throw new PropertyException("Invalid Price");
            }

            //Year Validation
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

            //If the new property has a registered owner 
            if (idOwner != null)
            {
                //Getting Owner Data
                var owner = _db.Owners.FirstOrDefault(o => o.IdOwner == idOwner.Value);

                //If the owner doesn't exist
                if (owner == null)
                {
                    throw new PropertyException("Invalid Owner");
                }//If the owner exist
                else
                {
                    newProperty.PropertyOwner = owner;
                }
            }


            //Saving data
            _db.Properties.Add(newProperty);
            _db.SaveChanges();

            //Mapping the data to return 
            return _mapper.Map<PropertyDto>(newProperty);
        }

        /// <summary>
        /// Get the properties with filters
        /// </summary>
        /// <param name="filter">Filters to get the properties</param>
        /// <returns>Returns the list of properties of the query</returns>
        public IList<PropertyDto> GetProperties(PropertyFilter filter)
        {
            //Creating the list to send
            var properties = new List<PropertyDto>();

            //Getting the properties by selected filters 
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

            //Adding properties to the list
            foreach (var item in lst)
            {
                properties.Add(_mapper.Map<PropertyDto>(item));
            }

            return properties;
        }

        /// <summary>
        /// Get property by ID
        /// </summary>
        /// <param name="idProperty">Property ID</param>
        /// <returns>Property with it's data</returns>
        public PropertyDto GetPropertyById(int idProperty)
        {
            //Getting the property from the method 'GetProperty' by ID
            var property = GetProperty(idProperty);

            //Mapping the data to return 
            return _mapper.Map<PropertyDto>(property);
        }

        /// <summary>
        /// Change de property price
        /// </summary>
        /// <param name="idProperty">Property ID</param>
        /// <param name="price">New Price</param>
        /// <returns>Property with it's data</returns>
        /// <exception cref="PropertyException"></exception>
        public PropertyDto ChangePrice(int idProperty, decimal price)
        {
            //Price Validation
            if (price <= 0)
            {
                throw new PropertyException("Invalid Price");
            }

            //Getting the property from the method 'GetProperty' by ID
            var property = GetProperty(idProperty);

            //Change Price
            property.Price = price;

            //Saving data
            _db.Properties.Update(property);
            _db.SaveChanges();

            //Mapping the data to return 
            return _mapper.Map<PropertyDto>(property);
        }

        /// <summary>
        /// Update Property
        /// </summary>
        /// <param name="idProperty">Property ID</param>
        /// <param name="property">Data for update the property</param>
        /// <returns>Property with it's data</returns>
        public PropertyDto Update(int idProperty, PropertyDto property)
        {
            //Property data validation
            if (property == null)
            {
                throw new PropertyException("Invalid Property Data To Update");
            }

            //Getting the property from the method 'GetProperty' by ID
            var propertyToUpdate = GetProperty(idProperty);

            //Name validation to update
            if (!String.IsNullOrEmpty(property.Name) && !property.Name.Equals(propertyToUpdate.Name))
            {
                propertyToUpdate.Name = property.Name;
            }

            //Address validation to update
            if (!String.IsNullOrEmpty(property.Address) && !property.Address.Equals(propertyToUpdate.Address))
            {
                propertyToUpdate.Address = property.Address;
            }

            ////Year validation to update
            if (property.Year >= 1900 && property.Year != propertyToUpdate.Year)
            {
                propertyToUpdate.Year = property.Year;
            }

            ////Owner validation to update
            if (property.PropertyOwner != null && property.PropertyOwner.IdOwner > 0 && property.PropertyOwner.IdOwner != propertyToUpdate.IdOwner)
            {
                //Getting Owner Data
                var owner = _db.Owners.FirstOrDefault(o => o.IdOwner == property.PropertyOwner.IdOwner);

                //If the owner doesn't exist
                if (owner == null)
                {
                    throw new PropertyException("Invalid Owner");
                }
                //If the owner exist
                else
                {
                    propertyToUpdate.PropertyOwner = owner;
                }
            }

            //Update Data
            _db.Properties.Update(propertyToUpdate);
            _db.SaveChanges();

            //Mapping the data to return 
            return _mapper.Map<PropertyDto>(propertyToUpdate);
        }

        /// <summary>
        /// Add a image from the property
        /// </summary>
        /// <param name="idProperty">Property ID</param>
        /// <param name="image">image to add to property</param>
        /// <returns>Property Images Data</returns>
        public PropertyImageDto AddImage(int idProperty, byte[] image)
        {
            //Image File Validation
            if (image == null || image.Length < 1)
            {
                throw new PropertyException("Invalid Image");
            }

            //Getting the property from the method 'GetProperty' by ID
            var propety = GetProperty(idProperty);

            //Saving Image And Getting Its Name Using The Helper Method
            var imageName = ImageHelper.AddImage(propety.CodeInternal, image);

            //Creating the property image
            var propertyImage = new PropertyImage
            {
                Property = propety,
                Image = imageName
            };

            //Saving DATA
            _db.PropertyImages.Add(propertyImage);
            _db.SaveChanges();

            //Mapping the data to return 
            return _mapper.Map<PropertyImageDto>(propertyImage);

        }


        /// <summary>
        /// Get the image of the property
        /// </summary>
        /// <param name="idProperty">Property ID</param>
        /// <param name="idPropertyImage">Image ID</param>
        /// <returns>Property Image</returns>
        public byte[] GetImage(int idProperty, int idPropertyImage)
        {
            //Getting the property from the method 'GetProperty' by ID
            var property = GetProperty(idProperty);

            //Getting the property image data by ID
            var propertyImage = _db.PropertyImages.FirstOrDefault(im => im.IdProperty == idProperty && im.IdPropertyImage == idPropertyImage);

            //If the image data was not found
            if (propertyImage == null)
            {
                throw new PropertyException("Invalid Image");
            }

            //Getting The Image Using The Helper Method
            return ImageHelper.GetImage(property.CodeInternal, propertyImage.Image);
        }

        #region
        /// <summary>
        /// Get Property By ID
        /// </summary>
        /// <param name="id">Property ID</param>
        /// <returns>Property Selected</returns>
        /// <exception cref="PropertyException"></exception>
        private Property GetProperty(int id)
        {
            //Finding the property with its data by ID
            var property = _db.Properties.Include("PropertyOwner")
                                         .Include("PropertyImages")
                                         .FirstOrDefault(p => p.IdProperty == id);

            //If the property doesn't exist
            if (property == null)
            {
                //Send error 
                throw new PropertyException("Invalid Property");
            }

            //Send property
            return property;
        }


        #endregion
    }
}
