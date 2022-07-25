using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public static class SaveGame
{
    public static readonly string SAVE_FOLDER = Application.dataPath + "/Saves/";

    public static void Init()
    {
        if (!Directory.Exists(SAVE_FOLDER))
        {
            Directory.CreateDirectory(SAVE_FOLDER);
        }
    }

    public static void Save(string saveString)
    {
        File.WriteAllText(SAVE_FOLDER + "save.txt", saveString);
    }

    public static string Load()
    {
        if (File.Exists(SAVE_FOLDER + "save.txt"))
        {
            string saveString = File.ReadAllText(SAVE_FOLDER + "save.txt");
            return saveString;
        }
        else
        {
            return null;
        }
    }

}

public class SaveObject
{
    public int highestScore = 0;
}