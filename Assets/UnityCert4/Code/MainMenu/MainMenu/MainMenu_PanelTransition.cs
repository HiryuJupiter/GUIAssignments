using UnityEngine;
using System.Collections;

public class MainMenu_PanelTransition : MonoBehaviour
{
    [Range(0f, 1f)]
    public float TransitionDuration = 0.2f;

    [SerializeField] CanvasGroup MainMenu;
    [SerializeField] CanvasGroup OptionsMenu;

    bool inTransition = false;

    #region Initialization
    void Awake()
    {
        CanvasGroupHelper.InstantHide(MainMenu);
        CanvasGroupHelper.InstantHide(OptionsMenu);
        StartCoroutine(DelayedInitialFade());
    }

    IEnumerator DelayedInitialFade()
    {
        yield return new WaitForSeconds(0.2f);
        StartCoroutine(CanvasGroupHelper.CanvasFadeIn(MainMenu, TransitionDuration));
    }
    #endregion

    #region Public - transition call
    public void MainToOptions()
    {
        Debug.Log("MainToOptions");
        if (!inTransition)
        {
            StartCoroutine(CanvasGroupHelper.CanvasesCrossfade(MainMenu, OptionsMenu, TransitionDuration));
        }
    }

    public void OptionsToMain()
    {
        Debug.Log("OptionsToMain");
        if (!inTransition)
        {
            StartCoroutine(CanvasGroupHelper.CanvasesCrossfade(OptionsMenu, MainMenu, TransitionDuration));
        }
    }
    #endregion
}