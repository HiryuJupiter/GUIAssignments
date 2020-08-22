using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GradientHealth_test1 : MonoBehaviour
{
    [Header("Scene objects")]
    public Transform followObj;

    [Header("UI")]
    public Image healthBarFG;
    public float currentHealth;
    public float maxHealth;
    public Gradient gradient;

    RectTransform myTransform;

    void Start()
    {
        myTransform = GetComponent<RectTransform>();
    }

    void Update()
    {
        FollowTarget();

        //Debug
        if (Input.GetKeyDown(KeyCode.D))
        {
            currentHealth -= 5;
        }
    }

    public void SetHealth(float health)
    {
        healthBarFG.fillAmount = Mathf.Clamp01(health / maxHealth);
        healthBarFG.color = gradient.Evaluate(healthBarFG.fillAmount);
    }

    void FollowTarget ()
    {
        //This is working
        Vector3 worldPos = Camera.main.WorldToScreenPoint(followObj.position);
        myTransform.transform.position = worldPos;
    }
}
