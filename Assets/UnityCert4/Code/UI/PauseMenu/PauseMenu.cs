using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PauseMenuCanvasTransition))]
public class PauseMenu : MonoBehaviour
{
    PauseMenuCanvasTransition transition;
    SceneForestManager sceneManager;
    bool isPaused = false;

    #region MonoBehavior
    void Start()
    {
        transition = GetComponent<PauseMenuCanvasTransition>();
        sceneManager = SceneForestManager.Instance;
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
    public void TogglePause()
    {
        isPaused = !isPaused;
        SetPause(isPaused);
    }

    public void OpenOptionsMenu ()
    {
        transition.SetVisibility_OptionsMenu(true);
    }

    public void CloseOptionsMenu ()
    {
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