using System.ComponentModel.DataAnnotations;

namespace BackendTestApp.API.Request
{
    /// <summary>
    /// Create Property Request
    /// </summary>
    public class CreatePropertyRequest
    {
        /// <summary>
        /// Property Name
        /// </summary>
        [Required]
        public string Name { get; set; } = null!;

        /// <summary>
        /// Property Address
        /// </summary>
        [Required]
        public string Address { get; set; } = null!;

        /// <summary>
        /// Property Price
        /// </summary>
        [Required]
        public decimal Price { get; set; }

        /// <summary>
        /// Property Taxable Year
        /// </summary>
        [Required]
        public int Year { get; set; }

        /// <summary>
        /// Property Owner ID
        /// </summary>
        public int? IdOwner { get; set; }
    }
}
