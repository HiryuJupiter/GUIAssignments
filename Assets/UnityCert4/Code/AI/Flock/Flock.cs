using UnityEngine;
using System.Collections;
using Boo.Lang;

public class Flock : MonoBehaviour
{
    public FlockAgent agentPrefab;
    public FlockBehavior behavior;

    List<FlockAgent> agents = new List<FlockAgent>();    

    [Range(10, 500)]
    public int startingCount = 250;
    const float AgentDensity = 0.08f;

    [Range(1f, 100f)]
    public float driveFactor = 10f; //Multiplier for agent movespeed
    [Range(1f, 100f)]
    public float maxSpeed = 5f;
    [Range(1f, 10f)]
    public float neighborRadius = 1.5f;
    [Range(0f, 1f)]
    public float avoidanceRadiusMultiplier = 0.5f;

    //Cache for performance
    float squareMaxSpeed;
    float squareNeighborRadius;
    public float SquareAvoidanceRadius { get; private set; }

    void Start ()
    {
        squareMaxSpeed = maxSpeed * maxSpeed;
        squareNeighborRadius = neighborRadius * neighborRadius;
        SquareAvoidanceRadius = squareNeighborRadius * avoidanceRadiusMultiplier * avoidanceRadiusMultiplier;

        for (int i = 0; i < startingCount; i++)
        {
            FlockAgent newAgent = Instantiate(
                agentPrefab, 
                Random.insideUnitCircle * startingCount * AgentDensity,
                Quaternion.Euler(Vector3.forward * Random.Range(0, 360f)),
                transform
                );
            newAgent.name = "Agent" + i;
            newAgent.Initialize(this);
            agents.Add(newAgent);
        }
    }    
}