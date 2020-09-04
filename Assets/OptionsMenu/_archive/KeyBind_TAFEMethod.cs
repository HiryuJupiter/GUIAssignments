using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

namespace MainMenu
{
    public class KeyBind_TAFEMethod : MonoBehaviour
    {
        static Dictionary<string, KeyCode> keybinds = new Dictionary<string, KeyCode>();

        public Text ui_up, ui_down, ui_left, ui_right, ui_jump;

        public Color color_selected;
        public Color color_normal;

        string stringkey_up = "Up";
        string stringkey_down = "Down";
        string stringkey_left = "Left";
        string stringkey_right = "Right";
        string stringkey_jump = "Jump";

        GameObject listeningButton;


        void Start()
        {
            //Give UI element the same name as stringkeys to ensure no mistakes are made.
            ui_up.name = stringkey_up;
            ui_down.name = stringkey_down;
            ui_left.name = stringkey_left;
            ui_right.name = stringkey_right;
            ui_jump.name = stringkey_jump;

            //Initialize dictionary
            keybinds = new Dictionary<string, KeyCode>()
        {
            {stringkey_up,    (KeyCode)PlayerPrefs.GetInt(stringkey_up, (int)KeyCode.W)},
            {stringkey_down,  (KeyCode)PlayerPrefs.GetInt(stringkey_down, (int)KeyCode.S)},
            {stringkey_left,  (KeyCode)PlayerPrefs.GetInt(stringkey_left, (int)KeyCode.A)},
            {stringkey_right, (KeyCode)PlayerPrefs.GetInt(stringkey_right, (int)KeyCode.D)},
            {stringkey_jump,  (KeyCode)PlayerPrefs.GetInt(stringkey_jump, (int)KeyCode.Space)},
        };

            //Initialize UI text contents
            ui_up.text = keybinds[stringkey_up].ToString();
            ui_down.text = keybinds[stringkey_down].ToString();
            ui_left.text = keybinds[stringkey_left].ToString();
            ui_right.text = keybinds[stringkey_right].ToString();
            ui_jump.text = keybinds[stringkey_jump].ToString();
        }

        void Update()
        {
            if (Input.GetKeyDown(keybinds[stringkey_up]))
            {
                Debug.Log("Debug Keybind: up key pressed");
            }
        }

        #region Listening for key 
        public void ChangeKey(GameObject button)
        {
            if (listeningButton == null) //If we're not currenting binding a key
            {
                listeningButton = button;
                listeningButton.GetComponent<Image>().color = color_selected;
            }
        }

        void OnGUI()
        {
            if (listeningButton != null)
            {
                //Listen to the key being pressed.
                string newKey = "";
                Event e = Event.current;

                if (e.isKey)
                {
                    newKey = e.keyCode.ToString();
                }
                else if (Input.GetKey(KeyCode.LeftShift)) //Shift key doesn't work with Event 
                {
                    newKey = "LeftShift";
                }
                else if (Input.GetKey(KeyCode.RightShift))
                {
                    newKey = "RightShift";
                }
                if (newKey != "")
                {
                    //Player entered a valid key.
                    Debug.Log("Player entered key: " + e.keyCode + " for button " + listeningButton.name);
                    keybinds[listeningButton.name] = e.keyCode;
                    //keybind[listeningButton.name] = (KeyCode)System.Enum.Parse(typeof(KeyCode), newKey);
                    listeningButton.GetComponentInChildren<Text>().text = newKey;
                    listeningButton.GetComponent<Image>().color = color_normal;
                    listeningButton = null;
                }
            }
        }
        #endregion

        #region Playerpref Save / Load / Reset
        void SaveAllKeys()
        {
            foreach (var k in keybinds)
            {
                PlayerPrefs.SetInt(k.Key, (int)k.Value);
            }
            PlayerPrefs.Save();
        }

        void SaveKey(string stringKey, int keyCode)
        {
            PlayerPrefs.SetInt(stringKey, keyCode);
        }

        void LoadKeyMappings()
        {
            keybinds[stringkey_up] = (KeyCode)PlayerPrefs.GetInt(stringkey_up, (int)KeyCode.W);
            keybinds[stringkey_down] = (KeyCode)PlayerPrefs.GetInt(stringkey_down, (int)KeyCode.S);
            keybinds[stringkey_left] = (KeyCode)PlayerPrefs.GetInt(stringkey_left, (int)KeyCode.A);
            keybinds[stringkey_right] = (KeyCode)PlayerPrefs.GetInt(stringkey_right, (int)KeyCode.D);
            keybinds[stringkey_jump] = (KeyCode)PlayerPrefs.GetInt(stringkey_jump, (int)KeyCode.Space);
        }

        void ResetPlayerPref()
        {
            PlayerPrefs.SetInt(stringkey_up, (int)KeyCode.W);
            PlayerPrefs.SetInt(stringkey_down, (int)KeyCode.S);
            PlayerPrefs.SetInt(stringkey_left, (int)KeyCode.A);
            PlayerPrefs.SetInt(stringkey_right, (int)KeyCode.D);
            PlayerPrefs.SetInt(stringkey_jump, (int)KeyCode.Space);
        }
        #endregion
    }
}    