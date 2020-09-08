using UnityEngine;
using System.Collections;

public class GameManager : Singleton<GameManager>
{
    public GameData GameData { get; private set; }
    public bool loadeSaveFile = false;

    void Awake()
    {
        //Singleton
        DeleteDuplicateSingleton();
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
    }

    //Awake is called before OnLevelWasLoaded
    private void OnLevelWasLoaded(int level)
    {
        if (loadeSaveFile)
        {
            LoadGameData();
        }
    }

    void LoadGameData ()
    {
        loadeSaveFile = false;
        Debug.Log("GameManager calls SceneEvents.GameLoad");

        SceneEvents.GameLoad.CallEvent();
    }
}
