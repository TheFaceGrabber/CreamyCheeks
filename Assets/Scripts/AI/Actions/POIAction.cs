using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace CreamyCheaks.AI.Actions
{
    [CreateAssetMenu(menuName = "AI/Actions/POI")]
    public class POIAction : Action
    {
        public override void Run(FiniteStateMachine stateMachine)
        {
            if (stateMachine == null)
            {
                Debug.Log("stateMachine cannot be null... stopping POI action");
                return;
            }

            stateMachine.Agent.stoppingDistance = 0.1f;
            stateMachine.Agent.SetDestination(stateMachine.CurrentPOI.transform.position);
            stateMachine.HeadLookTarget = Vector3.zero;
            if (Vector3.Distance(stateMachine.CurrentPOI.transform.position, stateMachine.transform.position) <= 0.5f)
            {
                if (!stateMachine.IsAtPOI)
                    stateMachine.InStateForSeconds = 0;

                stateMachine.IsAtPOI = true;
                stateMachine.Animator.SetBool("IsWalking", false);

                stateMachine.WantedRotation = stateMachine.CurrentPOI.transform.rotation;
                stateMachine.CurrentPOI.IsInUse = true;
            }
            else
            {
                Vector3 loc = new Vector3(stateMachine.Agent.steeringTarget.x, stateMachine.transform.position.y,
                    stateMachine.Agent.steeringTarget.z);
                var dir = loc - stateMachine.transform.position;
                stateMachine.WantedRotation =
                    dir == Vector3.zero ? stateMachine.transform.rotation : Quaternion.LookRotation(dir);
                

                stateMachine.Animator.SetBool("IsWalking", true);
            }
        }
    }
}