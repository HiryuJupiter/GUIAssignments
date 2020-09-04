using UnityEngine;
using System.Collections;
using System.Security.Policy;

public class Player : MonoBehaviour
{
    public int level = 3;
    public int health = 100;

    void Start()
    {

    }

    void Update()
    {

    }

    public void Save()
    {
        SaveSystem.SavePlayerData(this);
    }

    public void Load ()
    {
        PlayerData.LoadData(this);
    }
}