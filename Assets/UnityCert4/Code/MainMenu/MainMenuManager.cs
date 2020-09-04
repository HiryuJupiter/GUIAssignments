
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor;

namespace MainMenu
{
    public class MainMenuManager : MonoBehaviour
    {
        const int SceneIndex_Running = 1;


        #region Public - Main menu
        public void PlayGame()
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(SceneIndex_Running);
        }

        public void Quit()
        {
#if UNITY_EDITOR
            EditorApplication.ExitPlaymode();
#endif
            Application.Quit();
        }
        #endregion
    }
}