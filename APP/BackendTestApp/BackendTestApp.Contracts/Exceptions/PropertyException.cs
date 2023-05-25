
namespace BackendTestApp.Contracts.Exceptions
{
    /// <summary>
    /// Property Exceptions
    /// </summary>
    public class PropertyException : Exception
    {
        public PropertyException() { }

        public PropertyException(string message) : base(message) { }
    }
}
