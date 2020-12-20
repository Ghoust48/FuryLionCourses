// Copyright (c) 2012-2019 FuryLion Group. All Rights Reserved.

using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class Storage
{
    public static void Save<T>(string filePath, T data)
    {
        try
        {
            File.WriteAllBytes(filePath, GetBytes(data));
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }

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

    private static byte[] GetBytes(object data)
    {
        using (var memoryStream = new MemoryStream())
        {
            new BinaryFormatter().Serialize(memoryStream, data);
            return memoryStream.ToArray();
        }
    }
}