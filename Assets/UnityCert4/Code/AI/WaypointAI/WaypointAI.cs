using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class WaypointAI : MonoBehaviour
{
    public float MoveSpeed = 5.0f;
    public Transform[] Waypoints;
    public Transform AiSprite;
    public float MinDistanceToWaypoint =  0.2f;
    public GameObject playerObject;
    public float ChasePlayerDistance;
    private int CurrentWaypoint = 0;

    int index = 0;

    void Update()
    {
        if (Vector2.Distance(playerObject.transform.position,
                     AiSprite.position)
                   < ChasePlayerDistance)
        {
            MoveAI(playerObject.transform.position);
        }
        else
        {
            Patrol();
        }
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
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, MoveSpeed * Time.deltaTime);
    }
}