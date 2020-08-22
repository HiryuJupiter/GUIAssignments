using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEditor;
using System.Security.Policy;

public class MainMenuManager : MonoBehaviour
{
    public Resolution[] resolutions;
    public Toggle toggle_fullScreen;
    public Dropdown resolutionDropDown;

    // Start is called before the first frame update
    void Start()
    {
        resolutions = Screen.resolutions;
        foreach (var item in resolutions)
        {
            Debug.Log(item);
        }
        

        //Populate the resolution dropdown box
        resolutionDropDown.ClearOptions();
        List<string> options = new List<string>();

        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            //Build a string for displaying the resolution
            string option = resolutions[i].width + "x" + resolutions[i].height;
            options.Add(option);

            //Find  the index of current resolution
            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }

            //Set up dropdown
            resolutionDropDown.AddOptions(options);
            resolutionDropDown.value = currentResolutionIndex;
            resolutionDropDown.RefreshShownValue();
        }
    }

    void SetResolution (int resolutionIndex)
    {
        Resolution res = resolutions[resolutionIndex];
        Screen.SetResolution(res.width, res.height, Screen.fullScreen);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #region Main menu options
    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        Debug.Log("qutting game");

#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#endif 
        Application.Quit();
    }
    #endregion

    #region Options menu options
    public void SetResolution (int width, int height)
    {

        Screen.SetResolution(width, height, Screen.fullScreen);
    }

    public void SetFullScreen(bool fullscreen)
    {
        Screen.fullScreen = fullscreen;
    }

    public void ChangeQuality(int index)
    {
        QualitySettings.SetQualityLevel(index);
    }
    #endregion


    #region Save Load

    public void SavePlayerPrefs()
    {
        //Save "fullscreen"
        if (Screen.fullScreen)
        {
            PlayerPrefs.SetInt("fullscreen", 1);
        }
        else
        {
            PlayerPrefs.SetInt("fullscreen", 0);
        }

        //Save "graphicsQuality"

    }

    public void LoadPlayerPrefs()
    {
        //Load "full screen"
        if (PlayerPrefs.HasKey("fullscreen"))
        {
            PlayerPrefs.SetInt("fullscreen", 1);
            Screen.fullScreen = true;
        }
        else
        {
            if (PlayerPrefs.GetInt("fullscreen") == 0)
            {
                SetFullScreen(false);
                toggle_fullScreen.isOn = false;
            }
            else
            {
                SetFullScreen(true);
                toggle_fullScreen.isOn = true;
            }
        }

        //Load "graphics quality"
    }
    #endregion


    void OnGUI ()
    {
        GUI.Label(new Rect(20, 20, 500, 20), QualitySettings.GetQualityLevel().ToString());
    }
}
