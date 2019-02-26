using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace CreamyCheaks.AI.Decisions
{
    /// <summary>
    /// Used to determine whether or not a Point Of Interest is near by
    /// </summary>
    [CreateAssetMenu(menuName = "AI/Decisions/POI")]
    public class POIDecision : Decision
    {
        public override bool Run(FiniteStateMachine stateMachine)
        {
            if (stateMachine == null)
            {
                return false;
            }
            if (stateMachine.DistanceLeft <= stateMachine.Agent.stoppingDistance &&
                stateMachine.CurrentRoom.RoomPOIs.Count > 0)
            {
                int r = Random.Range(0, stateMachine.CurrentRoom.RoomPOIs.Count - 1);
                stateMachine.CurrentPOI = stateMachine.CurrentRoom.RoomPOIs[r];
                return true;
            }

            return false;
        }
    }
}