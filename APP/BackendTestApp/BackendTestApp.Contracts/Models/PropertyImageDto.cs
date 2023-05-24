namespace BackendTestApp.Contracts.Models
{
    public class PropertyImageDto
    {
        public int IdPropertyImage { get; set; }

        public string Image { get; set; } = null!;

        public short Enabled { get; set; }
    }
}
