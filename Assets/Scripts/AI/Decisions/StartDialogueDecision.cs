using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace CreamyCheaks.AI.Decisions
{
    [CreateAssetMenu(menuName = "AI/Decisions/Start Dialogue")]
    public class StartDialogueDecision : Decision
    {
        public override bool Run(FiniteStateMachine stateMachine)
        {
            if (stateMachine == null)
            {
                return false;
            }
            return stateMachine.DoesPlayerWantToInteract;
        }
    }
}