using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Collider2D))]
public class FlockAgent : MonoBehaviour
{
    Flock agentFlock;
    Collider2D agentCollider;

    //Property
    public Flock AgentFlock { get{ return agentFlock;}}
    public Collider2D Collider { get { return agentCollider; } }

    void Start()
    {
        agentCollider = GetComponent<Collider2D>();
    }

    public void Initialize (Flock flock)
    {
        agentFlock = flock;
    }

    public void Move (Vector2 velocity)
    {
        transform.up = velocity;
        transform.position += (Vector3)velocity * Time.deltaTime;
    }
    
    void Update()
    {

    }
}