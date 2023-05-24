using BackendTestApp.Contracts.Exceptions;
using System.Reflection;

namespace BackendTestApp.Business.Helpers
{
    /// <summary>
    /// Helper for Property Images
    /// </summary>
    public class ImageHelper
    {
        /// <summary>
        /// Image Storage Path
        /// </summary>
        private static readonly string PATH = "IMAGES";


        /// <summary>
        /// Save Image
        /// </summary>
        /// <param name="codeProperty">Property Internal Code</param>
        /// <param name="image">Image to save by bytes</param>
        /// <returns>Name of the image in the storage</returns>
        /// <exception cref="PropertyException"></exception>
        public static string AddImage(string codeProperty, byte[] image)
        {
            //Getting the path location of the service
            var fullPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            //Set the full path where the image will be saved
            fullPath = String.Format("{0}\\{1}\\{2}\\", fullPath, PATH, codeProperty);

            //If the path doesn't existe
            if (!Directory.Exists(fullPath))
            {
                //Create new path
                Directory.CreateDirectory(fullPath);
            }

            //Generate the name of the image 
            var imageName = DateTime.Now.Ticks.ToString();

            //Set the full path of the image
            var fullFilePath = Path.Combine(fullPath, imageName);

            //Saving the image
            using (var stream = new FileStream(fullFilePath, FileMode.Create))
            {
                foreach (var byteFile in image)
                {
                    stream.WriteByte(byteFile);
                }

                // Set the stream position to the beginning of the file.
                stream.Seek(0, SeekOrigin.Begin);

                // Read and verify the data.
                for (int i = 0; i < image.Length; i++)
                {
                    //If the array byte is different from the save byte
                    if (image[i] != stream.ReadByte())
                    {
                        //Send error 
                        throw new PropertyException("Internal error trying to add the image");
                    }
                }
            }

            //Return the image name
            return imageName;
        }

        /// <summary>
        /// Get the selected image
        /// </summary>
        /// <param name="codeProperty">Property Internal Code</param>
        /// <param name="imageNamge">Image Name</param>
        /// <returns></returns>
        public static byte[] GetImage(string codeProperty, string imageNamge)
        {
            //Getting the path location of the service
            var fullPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            //Set the full path where the image will be saved
            fullPath = String.Format("{0}\\{1}\\{2}\\", fullPath, PATH, codeProperty);

            //Set the full path of the image
            var fullFilePath = Path.Combine(fullPath, imageNamge);

            //Getting the image 
            using (var stream = new FileStream(fullFilePath, FileMode.Open))
            {
                //Create the array to set the bytes of the image
                var imageFile = new byte[stream.Length];

                //Reading the image file
                for (int i = 0; i < stream.Length; i++)
                {
                    stream.Position = i;

                    //Setting the byte to the array
                    imageFile[i] = (byte)stream.ReadByte();
                }

                //Return the image file 
                return imageFile;
            }
        }
    }
}
