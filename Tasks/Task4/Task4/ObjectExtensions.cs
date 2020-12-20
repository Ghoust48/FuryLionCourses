using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Task4
{
    /// <summary>
    /// Расширения для object.
    /// </summary>
    public static class ObjectExtensions
    {
        /// <summary>
        /// Получение байтового представления объекта.
        /// </summary>
        /// <returns>Байтовое представление объекта</returns>
        public static byte[] GetBytes(this object data) 
        {
            using (var memoryStream = new MemoryStream())
            {
                new BinaryFormatter().Serialize(memoryStream, data);
                
                return memoryStream.ToArray();
            }
        }
    }
}