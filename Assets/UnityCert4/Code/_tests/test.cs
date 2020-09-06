using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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

    void Awake()
    {
        Debug.Log("awake");

    }

    void Start()
    {
        Debug.Log("start");

    }

    private void OnLevelWasLoaded(int level)
    {
        Debug.Log("loaded");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }


    private void OnDisable()
    {
        Debug.Log("disable");
    }
}