namespace BackendTestApp.API.Request
{
    public class EditPropertyRequest
    {
        public string? Name { get; set; } 
        public string? Address { get; set; }
        public int Price { get; set; }
        public int Year { get; set; }
        public int IdOwner { get; set; }
    }
}
