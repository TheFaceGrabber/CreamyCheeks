using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CreamyCheaks.AI.Actions;
using UnityEngine;

namespace CreamyCheaks.AI
{
    [CreateAssetMenu(menuName = "AI/State")]
    public class State : ScriptableObject
    {
        public Action Action;

        public Transition[] Transitions;

        public void Run(FiniteStateMachine stateMachine)
        {
            Action.Run(stateMachine);
        }

        public void RunEndConditions(FiniteStateMachine stateMachine)
        {
            for (int i = 0; i < Transitions.Length; i++)
            {
                bool decisionSucceeded = Transitions[i].Decision.Run(stateMachine);

                if (decisionSucceeded)
                {
                    if (Transitions[i].TrueState)
                    {
                        stateMachine.UpdateState(Transitions[i].TrueState);
                        stateMachine.InStateForSeconds = 0;
                        stateMachine.Agent.enabled = false;
                        stateMachine.Agent.enabled = true;
                    }
                }
            }

        }
    }
}