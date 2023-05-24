
namespace BackendTestApp.Contracts.Exceptions
{
    public class PropertyException : Exception
    {
        public PropertyException() { }

        public PropertyException(string message) : base(message) { }
    }
}
