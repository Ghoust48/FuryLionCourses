using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace ConsoleApp1
{
    /// <summary>
    /// Хранилище данных
    /// </summary>
    public class Storage
    {
        /// <summary>
        /// Сохранение объекта в файл
        /// </summary>
        /// <param name="filePath">Путь к файлу</param>
        /// <param name="data">Объект</param>
        /// <typeparam name="T">Тип объекта</typeparam>
        public static void Save<T>(string filePath, T data)
        {
            try
            {
                File.WriteAllBytes(filePath, data.GetBytes());
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        
        /// <summary>
        /// Загрузка объекта из файла
        /// </summary>
        /// <param name="filePath">Путь к файлу</param>
        /// <typeparam name="T">Тип объекта</typeparam>
        /// <returns></returns>
        public static T Load<T>(string filePath) where T : class, new()
        {
            try
            {
                using (var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                    return (T) new BinaryFormatter().Deserialize(fileStream);   
            }
            catch (FileNotFoundException fileNotFoundException)
            {
                Console.WriteLine(fileNotFoundException.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return new T();

        }
    }
}