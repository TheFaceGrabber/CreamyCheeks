using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace CreamyCheaks.AI.Actions
{
    [CreateAssetMenu(menuName = "AI/Actions/Dialogue")]
    public class DialogueAction : Action
    {
        public override void Run(FiniteStateMachine stateMachine)
        {
            if (stateMachine == null)
            {
                Debug.Log("stateMachine cannot be null... stopping dialogue action");
                return;
            }
            stateMachine.Animator.SetBool("IsWalking", false);
            var player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController.PlayerController>();

            stateMachine.Agent.isStopped = true;

            var dir = player.transform.position - stateMachine.transform.position;
            stateMachine.WantedRotation =
                dir == Vector3.zero ? stateMachine.transform.rotation : Quaternion.LookRotation(dir);
            stateMachine.HeadLookTarget = player.CameraTransform.position;
        }
    }
}