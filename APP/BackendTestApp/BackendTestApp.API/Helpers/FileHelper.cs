namespace BackendTestApp.API.Helpers
{
    /// <summary>
    /// File Helper 
    /// </summary>
    public class FileHelper
    {
        /// <summary>
        /// Get the bytes from a IFormFile 
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public static byte[] GetFileBytes(IFormFile file)
        {
            //Getting to stream to read the file
            var fileStream = file.OpenReadStream();
            
            //Create the array to set de bytes
            byte[] bytes = new byte[file.Length];

            //Reading the file and set the bytes in the array
            fileStream.Read(bytes, 0, (int)file.Length);

            //Send the array
            return bytes;
        }
    }
}
