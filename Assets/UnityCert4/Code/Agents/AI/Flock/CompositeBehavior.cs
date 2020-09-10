using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//Composite pattern??? weighted compsotiion?

[CreateAssetMenu(menuName = "Flock/Behavior/composite")]
public class CompositeBehavior : FlockBehavior
{
    [System.Serializable]
    public struct BehaviorGroup
    {
        public FlockBehavior behaviors;
        public float weights; //How much effect it has on the overall move
    }

    public BehaviorGroup[] behaviors;

    public override Vector2 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {
        Vector2 move = Vector2.zero;

        for (int i = 0; i < behaviors.Length; i++)
        {
            //Gets the calculate move method of each behavior attached
            Vector2 partialMove = behaviors[i].behaviors.CalculateMove(agent, context, flock) * behaviors[i].weights;


            if (partialMove != Vector2.zero)
            {
                //If the speed is greater than the weight, then normalize it
                if (partialMove.sqrMagnitude > behaviors[i].weights * behaviors[i].weights)
                {
                    partialMove.Normalize();
                    partialMove *= behaviors[i].weights;
                }

                //Add all behaviors together
                move += partialMove;
            }
        }

        return move;

        //CompositeBehavior.BehaviorGroup varName;
    }
}