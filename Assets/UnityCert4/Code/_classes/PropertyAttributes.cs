using System;
using System.Collections;
using System.Collections.Generic;
using System.Security;
using UnityEngine;


[System.Serializable]
public struct MyStruct
{
    public int x;
    public int y;
    public string s;
}

[SelectionBase]
public class PropertyAttributes : MonoBehaviour
{
    [Header("My struct")]
    public MyStruct myStruct;

    [Header("Deescriptions")]
    public string towerName = "Bob";

    [Space]
    [Header("Stats")]
    [SerializeField]
    [Range(-3, 3)]
    [Tooltip("This variable controls how fast the objet moves up and down")]
    private float speed;


    [Min(0)]
    public int cost;


    [TextArea]
    public string textArea;

    [Multiline]
    public string multipline;

    [Header("Context menu item")]
    [ContextMenuItem("Get random value", "Function_RandomValue")]
    public int randomValue;

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, 5f);
    }


    void Function_RandomValue ()
    {
        randomValue = UnityEngine.Random.Range(-10, 40);
    }

    [ContextMenu("Choose random value")]
    void RandomValueMenu ()
    {
        randomValue = UnityEngine.Random.Range(-10, 40);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
