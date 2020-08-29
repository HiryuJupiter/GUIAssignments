using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;


public class KeyScheme
{
    public static KeyCode Up;
    public static KeyCode Down;
    public static KeyCode Left;
    public static KeyCode Right;
    public static KeyCode Jump;
}

public class KeySchemeLookup
{

}

//Note: UI text elements must have the same name as the stringkeys.
public class KeyRemapper : MonoBehaviour
{
    #region Variables & Properties
    //PUBLIC
    [Header("UI Gameobjects")]
    public GameObject btn_up; 
    public GameObject btn_down;
    public GameObject btn_left;
    public GameObject btn_right;
    public GameObject btn_jump;

    //Exposed keycodes for easy access from outside.     
    KeyScheme keyScheme;

    //PRIVATE
    //Keys for Dictionary look up
    const string stringkey_up = "Up";
    const string stringkey_down = "Down";
    const string stringkey_left = "Left";
    const string stringkey_right = "Right";
    const string stringkey_jump = "Jump";

    //Keybind dictionary
    Dictionary<string, KeyCode> keybinds = new Dictionary<string, KeyCode>();

    //Caching for key change status
    GameObject  listeningButton;
    Text        listeningText;
    bool        waitingForKey = false;
    string      buttonOriginalString; //Cache the current button's original text so we can revert changes

    //PROPERTIES
    public KeyScheme GetKeyScheme { get { return keyScheme; } }
    #endregion

    #region Initialization
    void Start()
    {
        //Give UI element the same name as stringkeys for easier dictionary lookup.
        btn_up.name = stringkey_up;
        btn_down.name = stringkey_down;
        btn_left.name = stringkey_left;
        btn_right.name = stringkey_right;
        btn_jump.name = stringkey_jump;

        //Initialize dictionary
        keybinds = new Dictionary<string, KeyCode>()
        {
            { stringkey_up,   (KeyCode)PlayerPrefs.GetInt(stringkey_up, (int)KeyCode.W)},
            { stringkey_down,  (KeyCode)PlayerPrefs.GetInt(stringkey_down, (int)KeyCode.S)},
            { stringkey_left,  (KeyCode)PlayerPrefs.GetInt(stringkey_left, (int)KeyCode.A)},
            { stringkey_right, (KeyCode)PlayerPrefs.GetInt(stringkey_right, (int)KeyCode.D)},
            { stringkey_jump,  (KeyCode)PlayerPrefs.GetInt(stringkey_jump, (int)KeyCode.Space)},
        };

        UpdateAllUiTextDisplay();
    }

    void UpdateAllUiTextDisplay()
    {
        btn_up.GetComponentInChildren<Text>().text      = keybinds[stringkey_up].ToString();
        btn_down.GetComponentInChildren<Text>().text    = keybinds[stringkey_down].ToString();
        btn_left.GetComponentInChildren<Text>().text    = keybinds[stringkey_left].ToString();
        btn_right.GetComponentInChildren<Text>().text   = keybinds[stringkey_right].ToString();
        btn_jump.GetComponentInChildren<Text>().text    = keybinds[stringkey_jump].ToString();
    }
    #endregion

    #region Public - Reset keys
    public void ResetAllMapping()
    {
        PlayerPrefs.SetInt(stringkey_up, (int)KeyCode.W);
        PlayerPrefs.SetInt(stringkey_down, (int)KeyCode.S);
        PlayerPrefs.SetInt(stringkey_left, (int)KeyCode.A);
        PlayerPrefs.SetInt(stringkey_right, (int)KeyCode.D);
        PlayerPrefs.SetInt(stringkey_jump, (int)KeyCode.Space);

        UpdateAllUiTextDisplay();
    }
    #endregion

    #region Listening for key     
    public void ChangeKey(GameObject button)
    {
        if (!waitingForKey)
        {
            waitingForKey = true;

            //Update references
            listeningButton = button;
            listeningText = button.GetComponentInChildren<Text>();

            //Cache old text
            buttonOriginalString = listeningText.text;

            //Visual indication that we're listening for a key input
            listeningText.text = "???";

            StartCoroutine(ListenForKeyInput());
        }
    }

    IEnumerator ListenForKeyInput()
    {
        while (waitingForKey)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                waitingForKey = false;
                listeningText.text = buttonOriginalString;
                listeningText.color = Color.white;
                yield break;
            }

            if (Input.anyKeyDown)
            {
                foreach (KeyCode keycode in Enum.GetValues(typeof(KeyCode)))
                {
                    //If player pressed down a valid key.
                    //We use this instead of OnGUI's Event.Current in order to listen for Joypad.Buttons
                    if ((int)keycode < 323  && Input.GetKeyDown(keycode))
                    {
                        //Prevents setting the Quit-button that causes to quit the options menu on the same frame.
                        //StartCoroutine(menuM.PreventScreenChangeForOneFrame());

                        //Update UI Text
                        listeningText.text = keycode.ToString();

                        //Save (int)keycode to playerPref
                        PlayerPrefs.SetInt(listeningText.name, (int)keycode);
                        Debug.Log("Remapped " + listeningText.name + " button: " + keycode);

                        //Wrap up
                        waitingForKey = false;

                        yield break;
                    }
                }
            }
            yield return null;
        }
    }
    #endregion

    #region Public methods
    public void SaveKeybindChanges()
    {
        KeyScheme.Up = (KeyCode)PlayerPrefs.GetInt(stringkey_up, (int)KeyCode.A);
        KeyScheme.Down = (KeyCode)PlayerPrefs.GetInt(stringkey_down, (int)KeyCode.D);
        KeyScheme.Left = (KeyCode)PlayerPrefs.GetInt(stringkey_left, (int)KeyCode.W);
        KeyScheme.Right = (KeyCode)PlayerPrefs.GetInt(stringkey_right, (int)KeyCode.S);
        KeyScheme.Jump = (KeyCode)PlayerPrefs.GetInt(stringkey_jump, (int)KeyCode.J);
    }
    #endregion

    #region Playerpref Save / Load
    void SaveKeyschemeToPlayerpref()
    {
        PlayerPrefs.SetInt(stringkey_up,    (int)KeyScheme.Up);
        PlayerPrefs.SetInt(stringkey_down,  (int)KeyScheme.Down);
        PlayerPrefs.SetInt(stringkey_left,  (int)KeyScheme.Left );
        PlayerPrefs.SetInt(stringkey_right, (int)KeyScheme.Right);
        PlayerPrefs.SetInt(stringkey_jump,  (int)KeyScheme.Jump);
    }

    void LoadKeyschemeFromPlayerpref()
    {
        KeyScheme.Up = (KeyCode)PlayerPrefs.GetInt(stringkey_up, (int)KeyCode.W);
        KeyScheme.Down = (KeyCode)PlayerPrefs.GetInt(stringkey_down, (int)KeyCode.S);
        KeyScheme.Left = (KeyCode)PlayerPrefs.GetInt(stringkey_left, (int)KeyCode.A);
        KeyScheme.Right = (KeyCode)PlayerPrefs.GetInt(stringkey_right, (int)KeyCode.D);
        KeyScheme.Jump = (KeyCode)PlayerPrefs.GetInt(stringkey_jump, (int)KeyCode.Space);
    }    
    #endregion

}