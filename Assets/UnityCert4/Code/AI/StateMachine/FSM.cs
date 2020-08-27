using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSM : MonoBehaviour
{
    public enum State
    {
        crawl,
        walk,
        run,
    }
    public State state;
    public bool changeState = false;

    void Start()
    {
        NextState();
    }
    void NextState()
    {
        string methodName = state.ToString() + "State"; 

        System.Reflection.MethodInfo info =
            GetType().GetMethod(methodName,
                                System.Reflection.BindingFlags.NonPublic |
                                System.Reflection.BindingFlags.Instance);
        StartCoroutine((IEnumerator)info.Invoke(this, null));

    }

    IEnumerator CrawState ()
    {
        Debug.Log("Walk: Enter");
        yield return null;
        Debug.Log("Walk: Exit");
    }

    IEnumerator WalkState ()
    {
        yield return null;
    }

    IEnumerator RunState()
    {
        yield return null;
    }
}