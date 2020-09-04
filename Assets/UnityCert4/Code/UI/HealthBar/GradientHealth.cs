using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GradientHealth : MonoBehaviour
{
    [Header("References")]
    public RectTransform rectTransform;
    public Image healthBarFG;
    public Gradient gradient;
    public Transform followObj;

    [Header("Stats")]
    public float maxHealth = 100;

    float currentHealth = 100;

    public void InitializeStats(float maxHealth, Transform followObj)
    {
        this.maxHealth = maxHealth;
        currentHealth = maxHealth;
        this.followObj = followObj;
        Debug.Log("InitializeStats");
    }

    public void TakeDamage(int dmg)
    {
        currentHealth -= dmg;
        if (currentHealth < 0)
        {
            currentHealth = 0;
        }
    }

    #region Unity
    void Start()
    {
        Debug.Log("gradient hp Start");
        UpdateHealthBar();
    }


    void Update()
    {
        FollowTarget();

        //Debug
        if (Input.GetKeyDown(KeyCode.D))
        {
            TakeDamage(5);
        }
    }
    #endregion

    #region Health bar
    void UpdateHealthBar()
    {
        healthBarFG.fillAmount = Mathf.Clamp01(currentHealth / maxHealth);
        healthBarFG.color = gradient.Evaluate(healthBarFG.fillAmount);
    }

    void FollowTarget()
    {
        //This is working
        Vector3 worldPos = Camera.main.WorldToScreenPoint(followObj.position);
        rectTransform.transform.position = worldPos;
    }
    #endregion
}
