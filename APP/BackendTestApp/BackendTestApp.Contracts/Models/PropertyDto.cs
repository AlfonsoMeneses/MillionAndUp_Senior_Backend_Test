
namespace BackendTestApp.Contracts.Models
{
    /// <summary>
    /// Property Data Transfer Object
    /// </summary>
    public class PropertyDto
    {
        /// <summary>
        /// Property ID
        /// </summary>
        public int IdProperty { get; set; }

        /// <summary>
        /// Property Name
        /// </summary>
        public string Name { get; set; } = null!;

        /// <summary>
        /// Property Address
        /// </summary>
        public string Address { get; set; } = null!;

        /// <summary>
        /// Property Price
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Property Internal Code 
        /// </summary>
        public string CodeInternal { get; set; } = null!;

        /// <summary>
        /// Property Taxable Year
        /// </summary>
        public short Year { get; set; }

        /// <summary>
        /// Property Owner
        /// </summary>
        public OwnerDto PropertyOwner { get; set; } = null!;

        /// <summary>
        /// Property Images
        /// </summary>
        public ICollection<PropertyImageDto> PropertyImages { get; set; } = new List<PropertyImageDto>();
    }
}
