﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace CreamyCheaks.AI.Decisions
{
    [CreateAssetMenu(menuName = "AI/Decisions/End Dialogue")]
    public class EndDialogueDecision : Decision
    {
        public override bool Run(FiniteStateMachine stateMachine)
        {
            return !stateMachine.DoesPlayerWantToInteract;
        }
    }
}