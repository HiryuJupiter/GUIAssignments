using System;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using UnityEngine;
using UnityEngine.UI;
using System.Reflection.Emit;

public class test : MonoBehaviour
{
    [System.Serializable]
    public struct LookUpTest
    {
        public string keystring;
        public GameObject button;
        public KeyCode defaultKeycode;
        internal Text buttonText; //Added by code to save us the drag-and-drop in inspector.
    }

    public LookUpTest[] lookUp;

    void Start()
    {

    }
}