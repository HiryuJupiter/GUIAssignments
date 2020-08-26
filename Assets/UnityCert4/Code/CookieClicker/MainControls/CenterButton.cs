using System.Collections;
using System.Collections.Generic;
using System.Security;
using UnityEngine;


public struct MyStruct
{
    public int x;
    public int y;
    public string s;
}

[SelectionBase]
public class CenterButton : MonoBehaviour
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





    [TextArea]
    public string textArea;

    [Multiline]
    public string multipline;

    [Header("Context menu item")]


    


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
