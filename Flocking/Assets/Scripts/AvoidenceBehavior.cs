using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName= "Flock/Behaviour/Avoidence")]
public class AvoidenceBehavior : FlockBehaviour
{
    public override Vector3 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {
        if (context.Count == 0){
            return Vector3.zero;
        }
        Vector3 avoidenceMove = Vector3.zero;
        int nAvoid = 0;
        foreach (Transform item in context)
        {
            if (Vector3.SqrMagnitude(item.position - agent.transform.position) < flock.SquareAvoidanceRadius)
            {
                nAvoid++;
                avoidenceMove += (Vector3)(agent.transform.position - item.position);
            }
            avoidenceMove += (Vector3)item.position;
        }
        if (nAvoid > 0){
            avoidenceMove /= nAvoid;
        }
        return avoidenceMove;
    }

}
