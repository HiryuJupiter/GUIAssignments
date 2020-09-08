using UnityEngine;
using System.Collections;
using UnityEngine.Rendering;

public class MainMenu_CanvasTransition : MonoBehaviour
{
    [Range(0f, 1f)]
    public float TransitionDuration = 0.2f;

    [SerializeField] CanvasGroup Canvas_PressAnyKey;
    [SerializeField] CanvasGroup Canvas_MainMenu;
    [SerializeField] CanvasGroup Canvas_OptionsMenu;
    [SerializeField] CanvasGroup Canvas_LoadingScreen;

    bool inSplash = true;

    #region Initialization
    void Awake()
    {
        CanvasGroupHelper.InstantHide(Canvas_MainMenu);
        CanvasGroupHelper.InstantHide(Canvas_OptionsMenu);
        CanvasGroupHelper.InstantHide(Canvas_LoadingScreen);
        StartCoroutine(ShowSplashScreen());
    }

    IEnumerator ShowSplashScreen()
    {
        yield return new WaitForSeconds(0.2f);
        StartCoroutine(CanvasGroupHelper.CanvasFadeIn(Canvas_PressAnyKey, TransitionDuration));
    }
    #endregion

    #region Update
    private void Update()
    {
        if (inSplash && Input.anyKeyDown)
        {
            inSplash = false;
            SplashToMain();
        }
    }
    #endregion

    #region Public - transition call
    public void SplashToMain()
    {
        Debug.Log("SplashToMain");
        StartCoroutine(CanvasGroupHelper.CanvasesCrossfade(Canvas_PressAnyKey, Canvas_MainMenu, TransitionDuration));
    }

    public void MainToOptions()
    {
        Debug.Log("MainToOptions");
        StartCoroutine(CanvasGroupHelper.CanvasesCrossfade(Canvas_MainMenu, Canvas_OptionsMenu, TransitionDuration));
    }

    public void OptionsToMain()
    {
        Debug.Log("OptionsToMain");
        StartCoroutine(CanvasGroupHelper.CanvasesCrossfade(Canvas_OptionsMenu, Canvas_MainMenu, TransitionDuration));
    }

    public void MainToLoading()
    {
        Debug.Log("MainToLoading");
        StartCoroutine(CanvasGroupHelper.CanvasesCrossfade(Canvas_MainMenu, Canvas_LoadingScreen, TransitionDuration));
    }
    #endregion
}