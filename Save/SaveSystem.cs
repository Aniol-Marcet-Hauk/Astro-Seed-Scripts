
using System.IO;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
public enum Level
{
    chapter2 = 3,
    chapter3Boss = 5,
    chapter4 = 6
}
public static class SaveSystem 
{
    public static void Save(Vector3 player,int level,string name,int cutScene,int saveNum, float timer)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/savingFile" +saveNum +".geez";
        FileStream stream = new FileStream(path, FileMode.Create);
        SaveFileData data = new SaveFileData(player, level, name,cutScene,saveNum,timer);

        formatter.Serialize(stream, data);
        stream.Close();
    }
    public static void Save(Vector3 player, int level, string name, int cutScene, int saveNum, float timer, Chapter2 chp2)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/savingFile" + saveNum + ".geez";
        FileStream stream = new FileStream(path, FileMode.Create);
        SaveFileData data = new SaveFileData(player, level, name, cutScene, saveNum, timer, chp2);

        formatter.Serialize(stream, data);
        stream.Close();
    }
    public static void Save(Vector3 player, int level, string name, int cutScene, int saveNum, float timer, Chapter3Boss chp3Boss)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/savingFile" + saveNum + ".geez";
        FileStream stream = new FileStream(path, FileMode.Create);
        SaveFileData data = new SaveFileData(player, level, name, cutScene, saveNum, timer, chp3Boss);

        formatter.Serialize(stream, data);
        stream.Close();
    }
    public static void Save(Vector3 player, int level, string name, int cutScene, int saveNum, float timer, Chapter4 chp4)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/savingFile" + saveNum + ".geez";
        FileStream stream = new FileStream(path, FileMode.Create);
        SaveFileData data = new SaveFileData(player, level, name, cutScene, saveNum, timer, chp4);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static SaveFileData LoadData(int saveNum)
    {
        string path = Application.persistentDataPath + "/savingFile" +saveNum +".geez";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);


            SaveFileData data = formatter.Deserialize(stream) as SaveFileData;
            stream.Close();
            return data;
        }
        else
        {
            Debug.LogError("no save file in " + path);
            return null;
        }
    }
    public static void Delete(int saveNum)
    {
        string path = Application.persistentDataPath + "/savingFile" + saveNum + ".geez";
        if (File.Exists(path))
        {
            File.Delete(path);
        }
    }
}
