using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace CreamyCheaks.AI.Actions
{
    public abstract class Action : ScriptableObject
    {
        public abstract void Run(FiniteStateMachine stateMachine);
    }
}