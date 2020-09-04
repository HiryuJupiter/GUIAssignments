using UnityEngine;
using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

//This was set up so that the player will have no knowledge of the middleman save
//class, playerdata
public class SaveSystem : MonoBehaviour
{
    public static void SavePlayerData(Player player)
    {
        //Filestream: turns the class into characters and binary. It's called a stream because saving is always character by character
        BinaryFormatter formatter = new BinaryFormatter();

        //Creates the file name only
        string path = Application.persistentDataPath + "/player.sav";

        //
        //Opens/Accesses the file, opens it for you do things
        FileStream stream = new FileStream(path, FileMode.Create);

        //Create the data to be saved
        PlayerData data = PlayerData.ConvertToPlayerData(player);

        //Writes the data to the file
        //Converting the class into text, actually saving the file
        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static void LoadPlayerData (ref Player player)
    {
        string path = Application.dataPath + "/player.meme";

        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            PlayerData data = formatter.Deserialize(stream) as PlayerData;
            stream.Close();

            PlayerData.ConvertToPlayer(data, ref player);
        }
        else
        {
            Debug.LogError("Save file does not exist, using default.");
        }
    }
}