using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName= "Flock/Behaviour/Alignment")]
public class AlignmentBehavior : FlockBehaviour
{
     public override Vector3 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {
        if (context.Count == 0){
            return agent.transform.forward;
        }
        Vector3 alignmentMove = Vector3.zero;
        foreach (Transform item in context)
        {
            alignmentMove += (Vector3)item.transform.forward;
        }

        alignmentMove /= context.Count;

        //cohesionMove -= (Vector3)agent.transform.position;
        return alignmentMove;
    }

}
