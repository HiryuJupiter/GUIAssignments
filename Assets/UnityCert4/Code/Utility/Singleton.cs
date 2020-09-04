using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    protected static T instance;
    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<T>();

                if (instance == null)
                {
                    var singletonObject = new GameObject();
                    instance = singletonObject.AddComponent<T>();
                    singletonObject.name = typeof(T).ToString() + " (Singleton)";

                    DontDestroyOnLoad(singletonObject);
                }
            }
            return instance;
        }
    }

    protected void DeleteDuplicateSingleton()
    {
        if (instance != null && instance != this)
        {
            DestroyImmediate(gameObject);
            //Debug.Log("Duplicate " + typeof(T).ToString() + " has being destroyed.");
        }
    }
}

//if (instance == null)
//{
//    instance = this;
//    DontDestroyOnLoad(this);
//}
//else
//{
//    if (this != instance)
//    {
//        Destroy(this.gameObject);
//    }
//}