using UnityEngine;
using System.Collections;

public class GameData
{
    public int gameLevelIndex;
    //PLAYER
    public int playerLevel;
    public int playerHealth;
    public Vector3 playerPosition;


    #region Save to file & Load from file
    public void SaveGameData()
    {
        SaveSystem.SavePlayerData(this);
    }

    public void LoadGameData(int levelIndex = 0)
    {
        if (!SaveSystem.TryLoadPlayerData(this))
        {
            SetToDefault(levelIndex);
        }        
    }
    #endregion

    #region Default game data
    public void SetToDefault (int index)
    {
        switch (index)
        {
            default:
                gameLevelIndex = 1;
                playerLevel = 1;
                playerHealth = 100;
                playerPosition = new Vector3(0f, 0f, 0f);
                break;
        }
    }
    #endregion
}