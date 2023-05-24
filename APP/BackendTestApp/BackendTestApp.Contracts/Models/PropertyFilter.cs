
namespace BackendTestApp.Contracts.Models
{
    public class PropertyFilter
    {
        public string? Name { get; set; }
        public string? Address { get; set; }
        public decimal MinPrice { get; set; }
        public decimal MaxPrice { get; set; }
        public short FromYear { get; set; }
        public short ToYear { get; set; }
        public int IdOwner { get; set; }
    }
}
