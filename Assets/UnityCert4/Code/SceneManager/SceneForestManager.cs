using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneForestManager : Singleton<SceneForestManager>
{
    public GameStates GameState { get; protected set; }

    const int MainMenuIndex = 0;

    #region MonoBehavior
    protected void Awake()
    {
        DeleteDuplicateSingleton();
        instance = this;
    }


    void Start()
    {

    }

    void Update()
    {

    }

    void OnDisable()
    {
        SceneEvents.UnSubscribePerLevelEvents();
    }
    #endregion


    #region Public
    public void ReturnToMainMenu ()
    {
        SceneManager.LoadScene(MainMenuIndex);
    }
    #endregion
}