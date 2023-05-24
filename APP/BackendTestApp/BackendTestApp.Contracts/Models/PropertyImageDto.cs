namespace BackendTestApp.Contracts.Models
{
    /// <summary>
    /// Property Image Data Transfer Object
    /// </summary>
    public class PropertyImageDto
    {
        /// <summary>
        /// Image ID
        /// </summary>
        public int IdPropertyImage { get; set; }

        /// <summary>
        /// Property Image
        /// </summary>
        public string Image { get; set; } = null!;

        /// <summary>
        /// Define if the image is enable or not
        /// </summary>
        public short Enabled { get; set; }
    }
}
