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

        //Initialize
        SceneEvents.InitializeGameWideEvents();
        SceneEvents.InitializePerLevelEvents();
    }

    //Awake is called before OnLevelWasLoaded
    private void OnLevelWasLoaded(int level)
    {
        //Load game
        if (loadeSaveFile)
        {
            loadeSaveFile = false;
            Debug.Log("GameManager calls SceneEvents.GameLoad");

            SceneEvents.GameLoad.CallEvent();
        }
    }
}
