using BackendTestApp.Contracts.Models;

namespace BackendTestApp.Contracts.Services
{
    /// <summary>
    /// Service Contract
    /// </summary>
    public interface IPropertyService
    {
        /// <summary>
        /// Create a new property
        /// </summary>
        /// <param name="name">Property Name</param>
        /// <param name="address">Property Address</param>
        /// <param name="price">Property Price</param>
        /// <param name="year">Property Taxable Year</param>
        /// <param name="idOwner">Property Owner</param>
        /// <returns>Return the new property created</returns>
        PropertyDto Create(string name, string address, decimal price, int year, int? idOwner);

        /// <summary>
        /// Get the properties with filters
        /// </summary>
        /// <param name="filter">Filters to get the properties</param>
        /// <returns>Returns the list of properties of the query</returns>
        IList<PropertyDto> GetProperties(PropertyFilter filter);

        /// <summary>
        /// Get property by ID
        /// </summary>
        /// <param name="idProperty">Property ID</param>
        /// <returns>Property with it's data</returns>
        PropertyDto GetPropertyById(int idProperty);

        /// <summary>
        /// Update Property
        /// </summary>
        /// <param name="idProperty">Property ID</param>
        /// <param name="property">Data for update the property</param>
        /// <returns></returns>
        PropertyDto Update(int idProperty, PropertyDto property);

        /// <summary>
        /// Change de property price
        /// </summary>
        /// <param name="idProperty">Property ID</param>
        /// <param name="price">New Price</param>
        /// <returns></returns>
        PropertyDto ChangePrice(int idProperty, decimal price);

        /// <summary>
        /// Add a image from the property
        /// </summary>
        /// <param name="idProperty">Property ID</param>
        /// <param name="image">image to add to property</param>
        /// <returns></returns>
        PropertyImageDto AddImage(int idProperty, byte[] image);

        /// <summary>
        /// Get the image of the property
        /// </summary>
        /// <param name="idProperty">Property ID</param>
        /// <param name="idPropertyImage">Image ID</param>
        /// <returns></returns>
        byte[] GetImage(int idProperty, int idPropertyImage);
    }
}
