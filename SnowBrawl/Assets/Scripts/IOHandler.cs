using System.IO;
using UnityEngine;

public static class IOHandler
{
    public static void SaveFile(string fileName, object obj)
    {
        string path = GetPath(fileName);

        string jsonData = JsonUtility.ToJson(obj);

        File.WriteAllText(path, jsonData);
    }

    public static T LoadFile<T>(string fileName)
    {
        string path = GetPath(fileName);

        string data = File.ReadAllText(path);

        return JsonUtility.FromJson<T>(data);
    }

    public static bool FileExists(string fileName)
    {
        string path = GetPath(fileName);

        return File.Exists(path);
    }

    private static string GetPath(string fileName)
    {
        return Application.persistentDataPath + "/" + fileName;
    }
}
