﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor;
using System.Linq;
using UnityEngine.Animations;
using UnityEngine.Audio;

//Make sure the game's quality settings only have 3 options, as we won't be changing them here.
//Note 1: This code does not save the resolution set by the player because every time the game opens, it saves the 

namespace MainMenu
{
    public class MainMenuManager : MonoBehaviour
    {
        [Header("UI - Graphics")]
        public Dropdown Dropdown_Qualities;
        public Dropdown Dropdown_Resolutions;
        public Toggle FullscreenToggle;
        
        [Header("UI - Audio")]
        public AudioMixer mixer;
        public Slider musicSlider;
        public Slider SFXSlider;
        public Toggle MuteToggle;
        
        //Script reference
        SceneManager sceneManager;

        //Graphics
        List<Resolution> supportedResolutions;
        bool isFullscreen;
        int resolutionIndex = -1;
        int qualityIndex;

        //Audio
        const string Key_Fullscreen = "Fullscreen";
        const string Key_Quality    = "Quality";
        const string Key_Width      = "Width";
        const string Key_Height     = "Height";

        const string Key_MusicVol   = "MusicVol";
        const string Key_SFXVol     = "SFXVol";
        const string Key_mute       = "isMute";

        const int SceneIndex_Running = 1;
        const int mixerHighestValue = 20;
        const int mixerLowestValue = -80;

        #region MonoBehavior
        void Start()
        {
            Load_OptionsSettings();
        }

        //void OnGUI ()
        //{
        //    GUI.Label(new Rect(20, 20, 2000, 20), "Quality: " + QualitySettings.GetQualityLevel());
        //    GUI.Label(new Rect(20, 40, 2000, 20), "isFullscreen: " + isFullscreen);
        //}
        #endregion

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

        #region Public - Graphics
        public void SetResolution(int index)
        {
            //Note: we cannot put Dropdown_Resolutions.value in here, as when the dropdown (ui element)'s value is changed, it
            //will trigger it's onValueChange event, thus creating a circular loop. Therefore we can only directly set the
            //dropdown values outside.
            resolutionIndex = index;

            Screen.SetResolution(supportedResolutions[index].width, supportedResolutions[index].height, Screen.fullScreen);
        }

        public void SetQuality(int index)
        {
            qualityIndex = index;
            QualitySettings.SetQualityLevel(index);
        }

        public void SetFullScreen(bool b)
        {
            isFullscreen = b;
            Screen.fullScreen = b;
        }


        #endregion

        #region Load/Save
        public void Load_OptionsSettings()
        {
            //LOAD GRAPHICS
            //Load isFullscreen & set value in UI
            isFullscreen = PlayerPrefs.GetInt(Key_Fullscreen, Screen.fullScreen ? 1 : 0) == 1 ? true : false;
            //Debug.Log("isFullscreen " + isFullscreen);
            FullscreenToggle.isOn = isFullscreen;
            SetFullScreen(isFullscreen);

            //Load quality & set value in UI
            qualityIndex = PlayerPrefs.GetInt(Key_Quality, 0);
            Dropdown_Qualities.value = qualityIndex;
            SetQuality(qualityIndex);

            //Load resolution & set value in UI (Do not set resolution)
            PopulateResolutionDropdownBox();
            LoadResolution();

            //LOAD AUDIO
            LoadAudio();            
        }

        public void Save_OptionsSettings()
        {
            //Save isFullscreen
            PlayerPrefs.SetInt(Key_Fullscreen, isFullscreen == true ? 1 : 0);
            //Debug.Log("Save isFullscreen " + isFullscreen);
            //Debug.Log("PlayerPrefs.GetInt(Key_Fullscreen, Screen.fullScreen " + PlayerPrefs.GetInt(Key_Fullscreen, -1));

            //Save quality
            PlayerPrefs.SetInt(Key_Quality, qualityIndex);

            //Save resolution
            PlayerPrefs.SetInt(Key_Width, supportedResolutions[resolutionIndex].width);
            PlayerPrefs.SetInt(Key_Height, supportedResolutions[resolutionIndex].height);

            //Save audio
            PlayerPrefs.SetFloat(Key_MusicVol, musicSlider.value);
            PlayerPrefs.SetFloat(Key_SFXVol, SFXSlider.value);
            PlayerPrefs.SetInt(Key_mute, MuteToggle.isOn ? 1 : 0);
        }        
        #endregion

        #region Loading resolution
        void PopulateResolutionDropdownBox()
        {
            Dropdown_Resolutions.ClearOptions();

            //Goal: Go through all resolutions, save them as strings, then populate the dropdown box's options with it.
            //Note: We use the following line instead of "allResolutions = Screen.resolutions;" to prevent returning duplicates 
            //of the same resolution in the build version.
            supportedResolutions = Screen.resolutions.Where(resolution => resolution.refreshRate == 60).ToList();
            List<string> options = new List<string>();

            for (int i = 0; i < supportedResolutions.Count; i++)
            {
                options.Add(supportedResolutions[i].width + "x" + supportedResolutions[i].height);
            }

            Dropdown_Resolutions.AddOptions(options);
            Dropdown_Resolutions.RefreshShownValue();
        }

        //This method will try to load screen resolution by 
        //1. Load from player prefs
        //2. If player prefs doesn't contain a saved resolution data..
        //... then pick a supportedResolution that matches the screen.
        //3. If no supportedResolution matches the screen...
        //... then pick the largest supportedResolution (i.e. the last index).
        void LoadResolution()
        {
            if (supportedResolutions == null || supportedResolutions.Count == 0)
            {
                PopulateResolutionDropdownBox();
            }

            //1. Try load screen resolution from PlayerPrefs 
            int saved_w = PlayerPrefs.GetInt(Key_Width, 0);
            int saved_h = PlayerPrefs.GetInt(Key_Height, 0);

            if (saved_w != 0 && saved_h != 0)
            {
                Debug.Log("Has saved resolution data in PlayerPrefs.");
                for (int i = 0; i < supportedResolutions.Count; i++)
                {
                    if (saved_w == supportedResolutions[i].width &&
                        saved_h == supportedResolutions[i].height)
                    {
                        Dropdown_Resolutions.value = i;
                        SetResolution(i);

                        Debug.Log("Matching resolution data found in playerPrefs. Exiting LoadResolution().");
                        return;
                    }
                }
                Debug.Log("Saved resolution data was found but is not in the list of supportedResolutions.");
            }
            else
            {
                Debug.Log("No saved resolution setting.");
            }

            //2. See if there is a supported resolution that matches the screen.
            for (int i = 0; i < supportedResolutions.Count; i++)
            {
                if (Screen.currentResolution.width == supportedResolutions[i].width &&
                    Screen.currentResolution.height == supportedResolutions[i].height)
                {
                    Dropdown_Resolutions.value = i;
                    SetResolution(i);

                    Debug.Log("SupportedResolutions contains an option that matches the Screen.width and Screen.height. Exiting LoadResolution().");
                    return;
                }
            }
            Debug.Log("SupportedResolutions DOES NOT contain an option that matches the screen.");

            //3. Pick the largest supportedResolution
            Debug.Log("Picking the last option in supportedResolutions.");

            Dropdown_Resolutions.value = supportedResolutions.Count - 1;
            SetResolution(supportedResolutions.Count - 1);
        }
        #endregion

        #region Audio
        public void Set_MusicVolumn(float value)
        {
            mixer.SetFloat(Key_MusicVol, value);
        }

        public void Set_SfxVolumn(float value)
        {
            mixer.SetFloat(Key_SFXVol, value);
        }

        public void SetMute(bool b)
        {
            mixer.SetFloat(Key_mute, b ? mixerLowestValue : mixerHighestValue);
        }

        void LoadAudio()
        {
            //float masterVol = PlayerPrefs.GetFloat(Key_MasterVol, 0f);
            //masterSlider.value = masterVol;
            //mixer.SetFloat(Key_MasterVol, masterVol);

            float musicVol = PlayerPrefs.GetFloat(Key_MusicVol, 0f);
            musicSlider.value = musicVol;
            mixer.SetFloat(Key_MusicVol, musicVol);

            float SFXVol = PlayerPrefs.GetFloat(Key_SFXVol, 0f);
            SFXSlider.value = SFXVol;
            mixer.SetFloat(Key_SFXVol, SFXVol);

            bool isMute = PlayerPrefs.GetInt(Key_mute, 0) == 1;
            MuteToggle.isOn = isMute; //set ui
            mixer.SetFloat(Key_mute, isMute ? mixerLowestValue : mixerHighestValue); //set audio mixer
        }
        #endregion
    }
}