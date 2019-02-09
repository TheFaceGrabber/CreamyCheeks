using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/State")]
public class State : ScriptableObject
{
    public Action[] Actions;

    public Transition[] Transitions;

    public void Run(FiniteStateMachine stateMachine)
    {
        for (int i = 0; i < Actions.Length; i++)
        {
            Actions[i].Run(stateMachine);
        }
        for (int i = 0; i < Transitions.Length; i++)
        {
            bool decisionSucceeded = Transitions[i].Decision.Run(stateMachine);

            if (decisionSucceeded)
            {
                if(Transitions[i].TrueState)
                    stateMachine.UpdateState(Transitions[i].TrueState);
            }
            else
            {
                if (Transitions[i].FalseState)
                    stateMachine.UpdateState(Transitions[i].FalseState);
            }
        }
    }
}