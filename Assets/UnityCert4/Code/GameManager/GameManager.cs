using UnityEngine;
using System.Collections;


/* FLOW OF LOGIC FOR GAME LOADING
 Main menu 
> player clicks Continue game
> gameManager.Data loads game data and then sets bool loadeSaveFile to trut 
  to remind itself to call load event after the new scene is loaded.

Game Scene gets loaded
> GameManager sees it has loadSaveFile set to true, so it calls SceneEvent.Load
> All objects subscribed to Load to update their values by referencing 
  GameManager's Data.
 */

public class GameManager : Singleton<GameManager>
{
    public GameData Data { get; private set; }
    public bool loadeSaveFile = false;

    void Awake()
    {
        DeleteDuplicateSingleton();
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
    }

    private void OnLevelWasLoaded(int level)
    {
        if (loadeSaveFile)
        {
            loadeSaveFile = false;
            SceneEvents.GameLoad.CallEvent();
        }
    }
}
