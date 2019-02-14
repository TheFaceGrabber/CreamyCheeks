using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CreamyCheaks.AI.Decisions
{
    public class CanGoUpstairsDecision : Decision
    {
        public override bool Run(FiniteStateMachine stateMachine)
        {
            return false;
        }
    }
}