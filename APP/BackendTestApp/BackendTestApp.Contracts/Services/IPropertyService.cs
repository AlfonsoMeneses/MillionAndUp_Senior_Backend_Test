using BackendTestApp.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendTestApp.Contracts.Services
{
    public interface IPropertyService
    {
        PropertyDto Create(string name, string address, decimal price, int year, int? idOwner);

        IList<PropertyDto> GetProperties(PropertyFilter filter);

        PropertyDto ChangePrice(int idProperty, decimal price);
    }
}
