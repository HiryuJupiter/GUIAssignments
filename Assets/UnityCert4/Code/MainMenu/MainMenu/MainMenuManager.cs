
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor;

enum MainMenuState
{
    MainMenu,
    OptionsMenu,
} 

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] ContinueButton continueButton;
    [SerializeField] MainMenu_PanelTransition menuTransition;

    const int Scene_one = 1;

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
        SceneManager.LoadScene(Scene_one);
    }

    public void ContinueGame()
    {
        gm.loadeSaveFile = true;
        gm.Data.LoadGameData(Scene_one);
        SceneManager.LoadScene(Scene_one);
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