using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public abstract class Action : ScriptableObject
{
    public abstract void Run(FiniteStateMachine stateMachine);
}