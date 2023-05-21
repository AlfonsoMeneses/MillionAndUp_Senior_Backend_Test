using System.ComponentModel.DataAnnotations;

namespace BackendTestApp.API.Request
{
    public class CreatePropertyRequest
    {
        [Required]
        public string Name { get; set; } = null!;
        [Required]
        public string Address { get; set; } = null!;
        [Required]
        public decimal Price { get; set; }
        [Required]
        public int Year { get; set; }
        public int? IdOwner { get; set; }
    }
}
