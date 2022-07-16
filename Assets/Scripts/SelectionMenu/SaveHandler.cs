using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class SaveHandler
{
    public static void Save(string data)
    {
        File.WriteAllText(Application.persistentDataPath + "/shopData.json", data);
    }
    public static string Load()
    {
        if (File.Exists(Application.persistentDataPath + "/shopData.json"))
            return File.ReadAllText(Application.persistentDataPath + "/shopData.json");
        else
            return null;
    }

}
