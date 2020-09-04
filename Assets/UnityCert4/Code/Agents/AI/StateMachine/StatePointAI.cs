using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class StatePointAI : MonoBehaviour
{
    #region class variables
    public float speed = 5f;
    public GameObject[] Waypoint;
    public float minDistance = 0.5f;
    public float chasePlayerDistance = 5f;
    public int index = 0;
    public GameObject player;
    public TopDownPlayerController playerController; // == null
    #endregion

    public enum State//I added states for our ai
    {
        patrol,
        chase,
    }
    public State state;//I added states for our ai

    private void Start()
    {
        playerController = player.GetComponent<TopDownPlayerController>();
        NextState();
    }

    void Patrol()
    {
        float distance = Vector2.Distance(transform.position, Waypoint[index].transform.position);
        if (distance < minDistance)
        {
            index++;
        }
        if (index >= Waypoint.Length)
        {
            index = 0; ;
        }

        MoveAI(Waypoint[index].transform.position);
    }

    void MoveAI(Vector2 targetPosition)
    {
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
    }

    private void NextState()
    {
        string methodName = state.ToString() + "State"; 

        System.Reflection.MethodInfo info =
            GetType().GetMethod(methodName,
                                System.Reflection.BindingFlags.NonPublic |
                                System.Reflection.BindingFlags.Instance);

        StartCoroutine((IEnumerator)info.Invoke(this, null));
    }
}