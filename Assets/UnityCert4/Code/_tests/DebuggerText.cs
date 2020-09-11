using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DebuggerText : MonoBehaviour
{
    //public static DebuggerText instance;

    public Text t;
    OptionsMenu optionsMenu;

    private void Awake()
    {
        //instance = this;
    }

    private void Start()
    {
        optionsMenu = OptionsMenu.instance;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Screen.SetResolution(1920, 1080, optionsMenu.IsFullScreen);
            string s = $"Set resolution {1920},{1080}, {optionsMenu.IsFullScreen}. Current: {Screen.currentResolution.width}, {Screen.currentResolution.height}";
            DisplayText(s);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {

            Screen.SetResolution(1600, 900, optionsMenu.IsFullScreen);
            string s = $"Set resolution {1600},{900}, {optionsMenu.IsFullScreen}. Current: {Screen.currentResolution.width}, {Screen.currentResolution.height}";
            DisplayText(s);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Screen.SetResolution(1768, 992, optionsMenu.IsFullScreen);
            string s = $"Set resolution {1768},{900}, {optionsMenu.IsFullScreen}. Current: {Screen.currentResolution.width}, {Screen.currentResolution.height}";
            DisplayText(s);
        }
    }

    public void DisplayText (string s)
    {
        t.text = s;
    }

    //public static void Display_Text (string s)
    //{
    //    instance.DisplayText(s);
    //}
}