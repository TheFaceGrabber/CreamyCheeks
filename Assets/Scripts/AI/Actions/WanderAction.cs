using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.AI;
[CreateAssetMenu(menuName = "AI/Actions/Wander")]
public class WanderAction : Action
{
    public override void Run(FiniteStateMachine stateMachine)
    {
        if (stateMachine.Agent.remainingDistance == 0)
        {
            float maxWalkDist = 30;

            Vector3 randomDir = Random.insideUnitSphere * maxWalkDist;
            randomDir += stateMachine.transform.position;

            NavMeshHit hit;

            if (NavMesh.SamplePosition(randomDir, out hit, maxWalkDist, 1))
            {
                stateMachine.SetDestination(hit.position);
            }
        }
    }
}