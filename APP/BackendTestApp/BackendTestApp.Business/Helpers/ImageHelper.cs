using BackendTestApp.Contracts.Exceptions;
using System.Reflection;

namespace BackendTestApp.Business.Helpers
{
    public class ImageHelper
    {
        private static readonly string PATH = "IMAGES";

        /// <summary>
        /// Save Image
        /// </summary>
        /// <param name="codeProperty"></param>
        /// <param name="image"></param>
        /// <returns></returns>
        /// <exception cref="PropertyException"></exception>
        public static string AddImage(string codeProperty, byte[] image)
        {
            var fullPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            fullPath = String.Format("{0}\\{1}\\{2}\\", fullPath, PATH, codeProperty);

            if (!Directory.Exists(fullPath))
            {
                Directory.CreateDirectory(fullPath);
            }

            var imageName = DateTime.Now.Ticks.ToString();

            var fullFilePath = Path.Combine(fullPath, imageName);

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
                    if (image[i] != stream.ReadByte())
                    {
                        throw new PropertyException("Internal error trying to add the image");
                    }
                }
            }

            return imageName;
        }

        public static byte[] GetImage(string codeProperty, string imageNamge)
        {
            var fullPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            fullPath = String.Format("{0}\\{1}\\{2}\\", fullPath, PATH, codeProperty);

            var fullFilePath = Path.Combine(fullPath, imageNamge);

            using (var stream = new FileStream(fullFilePath, FileMode.Open))
            {
                var imageFile = new byte[stream.Length];
                for (int i = 0; i < stream.Length; i++)
                {
                    stream.Position = i;
                    imageFile[i] = (byte)stream.ReadByte();
                }

                return imageFile;
            }
        }
    }
}
