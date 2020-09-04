using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

namespace Assets.UnityCert4.Code.MainMenu.Loading
{
    public class SceneLoader : MonoBehaviour
    {
        //Issues with synchronous loading: no way of knowing when it's done. 
        //Synchornous loading locks up the main thread, where everything freezes until the scene is loaded, 
        //including your loading screen.

        public void LoadScene()
        {
            SceneManager.LoadScene("Loading");

            StartCoroutine(LoadAfterTimer());
        }

        IEnumerator LoadAfterTimer()
        {
            // the reason we use a coroutine is simply to avoid a quick "flash" of the 
            // loading screen by introducing an artificial minimum load time :boo:
            yield return new WaitForSeconds(2.0f);

            LoadScene("Game");
        }

        private void LoadScene(string sceneName)
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}