using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace CreamyCheaks.AI.Decisions
{
    public abstract class Decision : ScriptableObject
    {
        public abstract bool Run(FiniteStateMachine stateMachine);
    }
}