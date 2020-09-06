using UnityEngine;
using System.Collections;
/*
 Call order	Awake > SceneLoaded > Start
	
MainMenu	- GameManager Initialize both gameWide and sceneWide
Awake 	    - GameObjects can freely subscribe to sceneEvents (save, load)
Sceneloaded	- GameManager calls Load() if the load flag is true
Start	    - (unused)
OnDisable	- SceneManagers  unsubscribe PerlevelSceneEvents 
              It is still initialized, just unsubscribed all subscribers.
 */

public static class SceneEvents
{
    public static SceneEvent GameStart { get; private set; }
   
    public static SceneEvent PlayerSpawn { get; private set; }
    public static SceneEvent PlayerDead { get; private set; }
    public static SceneEvent GameQuit { get; private set; }

    public static SceneEvent GameSave { get; private set; }
    public static SceneEvent GameLoad { get; private set; }

    public static bool GameWideEventsInitialized { get; private set; }
    public static bool PerLevelEventsInitialized { get; private set; }

    public static void InitializeGameWideEvents()
    {
        GameSave = new SceneEvent();
        GameLoad = new SceneEvent();
    }

    public static void InitializePerLevelEvents()
    {
        GameStart = new SceneEvent();
        PlayerSpawn = new SceneEvent();
        PlayerDead = new SceneEvent();
        GameQuit = new SceneEvent();
    }

    public static void UnSubscribeAll_PerLevelEvents ()
    {
        GameStart.UnSubscribe();
        PlayerSpawn.UnSubscribe();
        PlayerDead.UnSubscribe();
        GameQuit.UnSubscribe();
    }
}