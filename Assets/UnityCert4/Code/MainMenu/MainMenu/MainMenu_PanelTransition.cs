using UnityEngine;
using System.Collections;

public class MainMenu_PanelTransition : MonoBehaviour
{
    [Range(0f, 5f)]
    public float TransitionDuration = 1f;

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
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(CanvasGroupHelper.CanvasFadeIn(MainMenu, TransitionDuration));
    }
    #endregion

    #region Public - transition call
    public void MainToOptions()
    {
        if (!inTransition)
        {
            StartCoroutine(CanvasGroupHelper.CanvasesCrossfade(MainMenu, OptionsMenu, TransitionDuration));
        }
    }

    public void OptionsToMain()
    {
        if (!inTransition)
        {
            StartCoroutine(CanvasGroupHelper.CanvasesCrossfade(OptionsMenu, MainMenu, TransitionDuration));
        }
    }
    #endregion
}