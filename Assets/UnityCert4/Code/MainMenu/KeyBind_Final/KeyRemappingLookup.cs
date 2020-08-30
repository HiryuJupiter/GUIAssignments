using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

/*
We use StringkeyInput to associate individual: 
a. UI buttons that the player press on to start rebinding a key
b. playerPref 
 
 
 */
namespace MainMenu.Keybind
{
    [System.Serializable]
    public class KeyRemappingLookup : MonoBehaviour
    {
        [Header("UI Gameobjects")]
        public GameObject Btn_up;
        public GameObject Btn_down;
        public GameObject Btn_left;
        public GameObject Btn_right;
        public GameObject Btn_jump;

        public Dictionary<string, KeyCode> finalKeycodes { get; private set; }
        Dictionary<string, GameObject> btns;
        Dictionary<string, Image> bgs;
        Dictionary<string, Text> uiTexts;

        public void Awake()
        {
            //Look up dictionary initializations
            finalKeycodes = new Dictionary<string, KeyCode>()
            {
                { Keystrings.Up,       KeyScheme.Up },
                { Keystrings.Down,     KeyScheme.Down },
                { Keystrings.Left,     KeyScheme.Left },
                { Keystrings.Right,    KeyScheme.Right },
                { Keystrings.Jump,     KeyScheme.Jump }
            };

            btns = new Dictionary<string, GameObject>()
            {
                { Keystrings.Up,       Btn_up},
                { Keystrings.Down,     Btn_down },
                { Keystrings.Left,     Btn_left },
                { Keystrings.Right,    Btn_right },
                { Keystrings.Jump,     Btn_jump }
            };

            uiTexts = new Dictionary<string, Text>()
            {
                { Keystrings.Up,       Btn_up.GetComponentInChildren<Text>()},
                { Keystrings.Down,     Btn_down.GetComponentInChildren<Text>() },
                { Keystrings.Left,     Btn_left.GetComponentInChildren<Text>() },
                { Keystrings.Right,    Btn_right.GetComponentInChildren<Text>() },
                { Keystrings.Jump,     Btn_jump.GetComponentInChildren<Text>() }
            };

            bgs = new Dictionary<string, Image>()
            {
                { Keystrings.Up,       Btn_up.GetComponent<Image>()},
                { Keystrings.Down,     Btn_down.GetComponent<Image>()},
                { Keystrings.Left,     Btn_left.GetComponent<Image>()},
                { Keystrings.Right,    Btn_right.GetComponent<Image>()},
                { Keystrings.Jump,     Btn_jump.GetComponent<Image>()}
            };
        }

        #region Public - Look ups & getter methods
        //public KeyCode GetKeyschemeKeycodes(string stringkey) => finalKeycodes[stringkey];

        public Text GetBtnText(string stringkey) => uiTexts[stringkey];

        public Image GetBtnBg(string stringkey) => bgs[stringkey];

        public string GetKeystringOfButton(GameObject gameObject) =>
            btns.FirstOrDefault(x => x.Value == gameObject).Key;
        #endregion
    }
}