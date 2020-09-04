using UnityEngine;
using System.Collections;

//A middle man for saving, since filestream doesnt handle
//unity data very well.
[System.Serializable]
public class PlayerData
{
    public int level;
    public int health;
    public float[] position;

    //public PlayerData(Player player)
    //{
    //    level = player.level;
    //    health = player.health;

    //    position = new float[3];
    //    position[0] = player.transform.position.x;
    //    position[1] = player.transform.position.y;
    //    position[2] = player.transform.position.z;
    //}

    public static void ConvertToPlayer(PlayerData data, ref Player player)
    {
        player.level = data.level;
        player.health = data.health;

        Vector3 pos = new Vector3(
            data.position[0],
            data.position[1],
            data.position[2]);
        player.transform.position = pos;
    }

    public static PlayerData ConvertToPlayerData(Player player)
    {
        PlayerData data = new PlayerData();
        data.level = player.level;
        data.health = player.health;

        data.position = new float[3];
        data.position[0] = player.transform.position.x;
        data.position[1] = player.transform.position.y;
        data.position[2] = player.transform.position.z;

        return data;
    }
}