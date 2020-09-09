using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SfxManager : MonoBehaviour
{
    public static SfxManager instance;
    [SerializeField]
    GameObject sfx_menuTransition;
    [SerializeField] 
    GameObject sfx_buttonClick;
    [SerializeField]
    GameObject sfx_uiQuietTick;

    bool canPlaySfx = false;

    #region MonoBehavior
    private void Awake()
    {
        instance = this;
    }

    IEnumerator Start()
    {
        //Delay canPlaySfx, because the on start, LoadOptionsSettings will change the 
        //options menu item's values and cause sfx to play.
        yield return new WaitForSeconds(0.2f);
        canPlaySfx = true;
    }
    #endregion

    #region Public
    public void SpawnUIButtonClick ()
    {
        if (canPlaySfx)
            instance.SpawnSfxPrefab(sfx_buttonClick);
    }

    public void SpawnQuietTick()
    {
        if (canPlaySfx)
            instance.SpawnSfxPrefab(sfx_uiQuietTick);
    }

    public void SpawnUIMenuTransition()
    {
        if (canPlaySfx)
            instance.SpawnSfxPrefab(sfx_menuTransition);
    }
    #endregion

    #region Private
    void SpawnSfxPrefab (GameObject pf)
    {
        Instantiate(pf, Vector3.zero, Quaternion.identity);
    }
    #endregion
}