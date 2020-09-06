using UnityEngine;
using System.Collections;

public abstract class SceneManagerBase : MonoBehaviour
{
    protected GameManager gm;

    void Awake()
    {
        gm = GameManager.Instance;
    }

    protected virtual void OnDisable()
    {
        SceneEvents.UnSubscribeAll_PerLevelEvents();
    }
}