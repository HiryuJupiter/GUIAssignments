using UnityEngine;
using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

//This was set up so that the player will have no knowledge of the middleman 
//GameDataSerializable
public class SaveSystem : MonoBehaviour
{
    public static void SavePlayerData(GameData gameData)
    {
        //Formatter converts a class into a stream, FileStream writes the stream to file
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(SavePath, FileMode.Create);
        GameDataSerializable data = GameDataSerializable.ConvertGameDataToSerializable(gameData);
        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static bool TryLoadPlayerData (GameData gameData)
    {
        string path = SavePath;

        if (HasSaveFile())
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            GameDataSerializable data = formatter.Deserialize(stream) as GameDataSerializable;
            stream.Close();

            GameDataSerializable.ConvertToGameData(data, gameData);
            return true;
        }
        else
        {
            Debug.LogError("Save file does not exist, using default.");
            return false;
        }
    }

    public static bool HasSaveFile ()
    {
        return File.Exists(SavePath);
    }

    static string SavePath => Application.persistentDataPath + "/player.save";
}