using UnityEngine;
using System.Collections;

public static class SceneEvents
{
    public static SceneEvent GameStart { get; private set; }
   
    public static SceneEvent PlayerSpawn { get; private set; }
    public static SceneEvent PlayerDead { get; private set; }
    public static SceneEvent GameQuit { get; private set; }

    public static SceneEvent GameSave { get; private set; }
    public static SceneEvent GameLoad { get; private set; }

    public static void Initiailize()
    {
        GameStart = new SceneEvent();        
        PlayerSpawn = new SceneEvent();
        PlayerDead = new SceneEvent();
        GameQuit = new SceneEvent();

        GameSave = new SceneEvent();
        GameLoad = new SceneEvent();
    }

    public static void UnSubscribeAll ()
    {
        GameStart.UnSubscribe();
        PlayerSpawn.UnSubscribe();
        PlayerDead.UnSubscribe();
        GameQuit.UnSubscribe();

        GameSave.UnSubscribe();
        GameLoad.UnSubscribe();
    }
}