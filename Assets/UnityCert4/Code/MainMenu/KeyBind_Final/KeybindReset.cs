﻿using UnityEngine;
using System.Collections;

namespace MainMenu.Keybind
{
    [RequireComponent(typeof(KeyRemapper))]
    public class KeybindReset : MonoBehaviour
    {
        public GameObject confirmMenu;

        KeyRemapper keyRemapper;

        void Awake()
        {
            keyRemapper = GetComponent<KeyRemapper>();
            CloseConfirmMenu();
        }

        public void OpenConfirmationMenu()
        {
            if (!keyRemapper.IsListeningForKey)
                confirmMenu.SetActive(true);
        }

        public void ConfirmReset()
        {
            Debug.Log("Key bindings have been reset.");
            KeyScheme.ResetAll();
            keyRemapper.UpdateAllUiDisplay();
            CloseConfirmMenu();
        }

        public void CloseConfirmMenu ()
        {
            confirmMenu.SetActive(false);
        }
    }
}