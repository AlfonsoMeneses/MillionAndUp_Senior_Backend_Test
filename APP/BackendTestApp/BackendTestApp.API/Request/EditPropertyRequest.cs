namespace BackendTestApp.API.Request
{
    /// <summary>
    /// Edit Property Request
    /// </summary>
    public class EditPropertyRequest
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
        /// Property Price
        /// </summary>
        public int Price { get; set; }

        /// <summary>
        /// Property Taxable Year
        /// </summary>
        public int Year { get; set; }

        /// <summary>
        /// Property Owner ID
        /// </summary>
        public int IdOwner { get; set; }
    }
}
