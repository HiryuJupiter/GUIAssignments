using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PauseMenuCanvasTransition))]
public class PauseMenu : MonoBehaviour
{
    PauseMenuCanvasTransition transition;
    SceneForestManager sceneManager;
    SfxManager sfxManager;
    bool isPaused = false;

    #region MonoBehavior
    void Start()
    {
        transition = GetComponent<PauseMenuCanvasTransition>();
        sceneManager = SceneForestManager.Instance;
        sfxManager = SfxManager.instance;

        SetPause(false);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    void OnDisable()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    #endregion


    #region Public - Click unity events
    public void OpenOptionsMenu ()
    {
        sfxManager.SpawnUIMenuTransition();
        transition.SetVisibility_OptionsMenu(true);
    }

    public void CloseOptionsMenu ()
    {
        sfxManager.SpawnUIMenuTransition();
        transition.SetVisibility_OptionsMenu(false);
    }

    public void QuitGame()
    {
        sceneManager.ReturnToMainMenu();
        isPaused = false;
        SetPause(isPaused);
    }
    #endregion

    #region Pause logic
    void TogglePause()
    {
        isPaused = !isPaused;
        SetPause(isPaused);
        sfxManager.SpawnUIMenuTransition();
    }

    void SetPause(bool pause)
    {
        isPaused = pause;
        transition.SetVisibility_PauseMenu(isPaused);
        if (isPaused)
        {
            Time.timeScale = 0f;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Time.timeScale = 1f;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
    #endregion
}