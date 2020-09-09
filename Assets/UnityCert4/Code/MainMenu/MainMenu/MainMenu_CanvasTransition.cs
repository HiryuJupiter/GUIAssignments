using UnityEngine;
using System.Collections;
using UnityEngine.Rendering;
using UnityEngine.XR.WSA.Input;

public class MainMenu_CanvasTransition : MonoBehaviour
{
    [Range(0f, 1f)]
    public float TransitionDuration = 0.2f;

    [SerializeField] CanvasGroup Canvas_PressAnyKey;
    [SerializeField] CanvasGroup Canvas_MainMenu;
    [SerializeField] CanvasGroup Canvas_OptionsMenu;
    [SerializeField] CanvasGroup Canvas_LoadingScreen;


    SfxManager sfxManager;

    bool inSplash = false;

    #region Initialization
    void Awake()
    {
        CanvasGroupHelper.InstantHide(Canvas_MainMenu);
        CanvasGroupHelper.InstantHide(Canvas_OptionsMenu);
        CanvasGroupHelper.InstantHide(Canvas_LoadingScreen);
        StartCoroutine(ShowSplashScreen());
    }

    private void Start()
    {
        sfxManager = SfxManager.instance;
    }

    IEnumerator ShowSplashScreen()
    {
        yield return new WaitForSeconds(0.2f);
        StartCoroutine(CanvasGroupHelper.CanvasFadeIn(Canvas_PressAnyKey, TransitionDuration));
        yield return new WaitForSeconds(TransitionDuration);
        inSplash = true;
    }
    #endregion

    #region Update
    private void Update()
    {
        if (inSplash && Input.anyKeyDown)
        {
            inSplash = false;
            //sfxManager.SpawnUIMenuTransition();
            SplashToMain();
        }
    }
    #endregion

    #region Public - transition call
    public void SplashToMain()
    {
        Debug.Log("SplashToMain");
        sfxManager.SpawnUIMenuTransition();
        StartCoroutine(CanvasGroupHelper.CanvasesCrossfade(Canvas_PressAnyKey, Canvas_MainMenu, TransitionDuration));
    }

    public void MainToOptions()
    {
        Debug.Log("MainToOptions");
        sfxManager.SpawnUIMenuTransition();
        StartCoroutine(CanvasGroupHelper.CanvasesCrossfade(Canvas_MainMenu, Canvas_OptionsMenu, TransitionDuration));
    }

    public void OptionsToMain()
    {
        Debug.Log("OptionsToMain");
        sfxManager.SpawnUIMenuTransition();
        StartCoroutine(CanvasGroupHelper.CanvasesCrossfade(Canvas_OptionsMenu, Canvas_MainMenu, TransitionDuration));
    }

    public void MainToLoading()
    {
        Debug.Log("MainToLoading");
        sfxManager.SpawnUIMenuTransition();
        StartCoroutine(CanvasGroupHelper.CanvasesCrossfade(Canvas_MainMenu, Canvas_LoadingScreen, TransitionDuration));
    }
    #endregion
}