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

    public void EnableButton ()
    {
        button.enabled = true;
        img.color = Color.white;
    }

    public void GreyOutButton ()
    {
        button.enabled = false;
        img.color = Color.grey;
    }
}