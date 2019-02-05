using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class State
{
    public Action[] Actions;

    public Transition[] Transitions;

    public void Update(FiniteStateMachine stateMachine)
    {
        throw new System.NotImplementedException();
    }
}