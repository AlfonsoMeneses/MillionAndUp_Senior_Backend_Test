namespace BackendTestApp.API.Helpers
{
    public class FileHelper
    {
        public static byte[] GetFileBytes(IFormFile file)
        {
            var fileStream = file.OpenReadStream();
            byte[] bytes = new byte[file.Length];

            fileStream.Read(bytes, 0, (int)file.Length);

            return bytes;
        }
    }
}
