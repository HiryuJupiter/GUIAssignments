
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] SceneLoader sceneLoader;
    const int SceneOne = 1;

    //MainMenuState state = MainMenuState.MainMenu;

    //Class reference
    GameManager gm;

    void Start()
    {
        gm = GameManager.Instance;
    }

    #region Public - Main menu
    public void StartNewGame()
    {
        sceneLoader.LoadLevel(SceneOne);
    }

    public void ContinueGame()
    {
        gm.loadeSaveFile = true;
        gm.GameData.LoadGameData();
        UnityEngine.SceneManagement.SceneManager.LoadScene(gm.GameData.gameLevelIndex);
    }

    public void Clicked_Quit()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#endif
        Application.Quit();
    }
    #endregion
}