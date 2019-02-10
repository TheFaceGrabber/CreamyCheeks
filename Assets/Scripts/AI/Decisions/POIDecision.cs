using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

/// <summary>
/// Used to determine whether or not a Point Of Interest is near by
/// </summary>
[CreateAssetMenu (menuName = "AI/Decisions/POI")]
public class POIDecision : Decision
{
    public override bool Run(FiniteStateMachine stateMachine)
    {
        var points = GameObject.FindGameObjectsWithTag("POI");

        return points.FirstOrDefault(x =>
                   Vector3.Distance(stateMachine.transform.position, x.transform.position) < 5 && !x.GetComponent<PointOfInterest>().IsInUse) != null;
    }
}