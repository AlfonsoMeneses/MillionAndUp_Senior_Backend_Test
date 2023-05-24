using BackendTestApp.Contracts.Models;

namespace BackendTestApp.Contracts.Services
{
    public interface IPropertyService
    {
        PropertyDto Create(string name, string address, decimal price, int year, int? idOwner);

        IList<PropertyDto> GetProperties(PropertyFilter filter);

        PropertyDto GetPropertyById(int idProperty);

        PropertyDto Update(int idProperty, PropertyDto property);

        PropertyDto ChangePrice(int idProperty, decimal price);

        PropertyImageDto AddImage(int idProperty, byte[] image);

        byte[] GetImage(int idProperty, int idPropertyImage);
    }
}
