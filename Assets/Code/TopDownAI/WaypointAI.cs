using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class WaypointAI : MonoBehaviour
{
    public float speed = 5.0f;
    public float MinDistance = 0.2f;
    public Transform[] Waypoints;

    int index = 0;

    void Update()
    {
        Patrol();
    }

    void Patrol ()
    {
        if (Vector2.Distance(transform.position, Waypoints[index].position) < 0.2f)
        {
            //When we reach waypoint, go to next waypoint
            index = (index >= Waypoints.Length - 1) ? 0 : index + 1;
        }

        MoveAI(Waypoints[index].position);
    }

    void MoveAI (Vector2 targetPosition)
    {
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
    }
}