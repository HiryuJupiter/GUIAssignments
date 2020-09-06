using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ContinueButton : MonoBehaviour
{
    [SerializeField] Button button;
    Image img;

    private void Awake()
    {
        img = button.gameObject.GetComponent<Image>();
    }

    private void Start()
    {
        RefreshDisplay();
    }

    public void RefreshDisplay ()
    {
        if (SaveSystem.HasSaveFile())
        {
            EnableButton();
        }
        else
        {
            GreyOutButton();
        }
    }

    void EnableButton ()
    {
        button.enabled = true;
        img.color = Color.white;
    }

    void GreyOutButton ()
    {
        button.enabled = false;
        img.color = Color.grey;
    }
}