using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawner : MonoBehaviour
{
    public Transform CanvasParent;

    public GameObject Pf_Enemy;
    public GameObject Pf_ui_gradientHealth;

    void Start()
    {
        
    }

    void Update()
    {
        //Debug   
        if (Input.GetKeyDown(KeyCode.O))
        {
            SpawnEnemy(transform.position, 100);
        }
    }

    void SpawnEnemy (Vector3 pos, float maxHP)
    {
        GameObject go = Instantiate(Pf_Enemy, pos, Quaternion.identity);        ;
        GameObject ui = Instantiate(Pf_ui_gradientHealth, pos, Quaternion.identity, CanvasParent);
        ui.GetComponent<GradientHealth>().InitializeStats(maxHP, go.GetComponent<Enemy>().headPoint);
    }
}
