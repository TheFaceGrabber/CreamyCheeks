using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CreamyCheaks.AI.Decisions
{
    [CreateAssetMenu(menuName = "AI/Decisions/Leave POI")]
    public class FinishPOIDecision : Decision
    {
        public override bool Run(FiniteStateMachine stateMachine)
        {
            if (stateMachine == null)
            {
                return false;
            }
            float timeSince = stateMachine.InStateForSeconds;
            bool r = stateMachine.IsAtPOI && timeSince > stateMachine.CurrentPOI.UseTime;

            if (r)
            {
                stateMachine.IsAtPOI = false;
                stateMachine.CurrentPOI = null;
                return true;
            }

            return false;
        }
    }
}