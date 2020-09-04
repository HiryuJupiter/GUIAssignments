using UnityEngine;
using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public static class SaveSystem2
{

    public static void SaveFile (Player player)
    {
        SaveFile(new PlayerData(player));
    }

    public static void SaveFile (PlayerData data)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        string path = Application.persistentDataPath + "/mySave.sav";
        FileStream stream = new FileStream(path, FileMode.Create);

        formatter.Serialize(stream, data);
                
        stream.Close();

    }
}