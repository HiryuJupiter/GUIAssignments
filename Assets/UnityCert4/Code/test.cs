using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using UnityEngine;

public class test : MonoBehaviour
{
    Dictionary<string, KeyCode> lookup;

    private void Update()
    {
        
    }

    private void OnGUI()
    {
        Debug.Log(Event.current);
    }

}
