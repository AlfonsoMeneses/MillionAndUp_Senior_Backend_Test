
namespace BackendTestApp.Contracts.Models
{
    public class PropertyDto
    {
        public int IdProperty { get; set; }

        public string Name { get; set; } = null!;

        public string Address { get; set; } = null!;

        public decimal Price { get; set; }

        public string CodeInternal { get; set; } = null!;

        public short Year { get; set; }

        public OwnerDto PropertyOwner { get; set; } = null!;

        public ICollection<PropertyImageDto> PropertyImages { get; set; } = new List<PropertyImageDto>();
    }
}
