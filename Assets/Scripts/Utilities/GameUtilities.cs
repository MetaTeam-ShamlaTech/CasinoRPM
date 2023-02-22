using Newtonsoft.Json;
using System.IO;

public class GameUtilities
{ 
    private static string filePath;

    public static string SerializeObject(object obj)
    {
        return JsonConvert.SerializeObject(obj);
    }

    public static var DeserializeObject<var>(string file)
    {
        return JsonConvert.DeserializeObject<var>(file);
    }

    public static string GetFileData(string path, string fileName)
    {
        filePath = path + "\\" + fileName;
        if (File.Exists(filePath))
            return File.ReadAllText(filePath);
        return null;
    }

    public static void SetFileData(string path, string fileName, string data)
    {
        filePath = path + "\\" + fileName;
        if (File.Exists(filePath))
            File.WriteAllText(filePath, data);
        else
        {
            File.Create(filePath);
            File.OpenWrite(filePath);
        }
    }
}