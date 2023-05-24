
namespace BackendTestApp.Contracts.Models
{
    /// <summary>
    /// Filters for property queries
    /// </summary>
    public class PropertyFilter
    {
        /// <summary>
        /// Property Name
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// Property Address
        /// </summary>
        public string? Address { get; set; }

        /// <summary>
        /// Minimum Price Of The Properties
        /// </summary>
        public decimal MinPrice { get; set; }

        /// <summary>
        /// Maximum Price Of The Properties
        /// </summary>
        public decimal MaxPrice { get; set; }

        /// <summary>
        /// Minimum Year
        /// </summary>
        public short FromYear { get; set; }

        /// <summary>
        /// Maximum Year
        /// </summary>
        public short ToYear { get; set; }

        /// <summary>
        /// Owner ID
        /// </summary>
        public int IdOwner { get; set; }
    }
}
