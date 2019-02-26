using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CreamyCheaks.AI.RoomSystem;
using UnityEngine;
using UnityEngine.AI;

namespace CreamyCheaks.AI.Actions
{
    [CreateAssetMenu(menuName = "AI/Actions/Wander")]
    public class WanderAction : Action
    {
        public override void Run(FiniteStateMachine stateMachine)
        {
            if (stateMachine == null)
            {
                Debug.Log("stateMachine cannot be null... stopping wander action");
                return;
            }

            Vector3 loc = new Vector3(stateMachine.Agent.steeringTarget.x, stateMachine.transform.position.y,
                stateMachine.Agent.steeringTarget.z);
            var dir = loc - stateMachine.transform.position;
            stateMachine.WantedRotation =
                dir == Vector3.zero ? stateMachine.transform.rotation : Quaternion.LookRotation(dir);

            stateMachine.HeadLookTarget = Vector3.zero;

            stateMachine.Agent.stoppingDistance = 1f;
            stateMachine.Animator.SetBool("IsWalking", true);
            if (stateMachine.DistanceLeft <= stateMachine.Agent.stoppingDistance)
            {
                var roomManager = GameObject.Find("RoomManager").GetComponent<RoomHandler>();
                stateMachine.LastRoom = stateMachine.CurrentRoom;
                stateMachine.CurrentRoom = roomManager.GetNextRoom(stateMachine.CurrentRoom, stateMachine.LastRoom);
                stateMachine.Agent.SetDestination(stateMachine.CurrentRoom.GetPoint().position);
            }

            RaycastHit hit;
            if (Physics.Raycast(stateMachine.transform.position, stateMachine.transform.forward, out hit, 1))
            {
                Door door = hit.collider.GetComponent<Door>();
                if (door != null)
                {
                    door.PlayerInteract();
                }
            }
        }
    }
}