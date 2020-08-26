using UnityEngine;
using System.Collections;

namespace Assets.Code.StringManipulation
{
    public class StringManipulation : MonoBehaviour
    {

        // Use this for initialization
        void Start()
        {
            string s = "MrMillsMr";
            s = s.Replace("Mr", "James");
            Debug.Log(s);
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}