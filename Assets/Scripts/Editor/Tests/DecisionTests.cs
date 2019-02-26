using System.Collections;
using System.Collections.Generic;
using CreamyCheaks.AI.Actions;
using CreamyCheaks.AI.Decisions;
using UnityEngine;
using NUnit.Framework;

public class DecisionTests
{
    [Test]
    public void EndDialogueDecisionTest()
    {
        EndDialogueDecision decision = 
            new EndDialogueDecision();
        Assert.That(decision.Run(null), Is.EqualTo(false));
    }

    [Test]
    public void FinishPOIDecisionTest()
    {
        FinishPOIDecision decision = 
            new FinishPOIDecision();
        Assert.That(decision.Run(null), Is.EqualTo(false));
    }

    [Test]
    public void POIDecisionTest()
    {
        POIDecision decision = 
            new POIDecision();
        Assert.That(decision.Run(null), Is.EqualTo(false));
    }

    [Test]
    public void StartDialogueDecisionTest()
    {
        StartDialogueDecision decision = 
            new StartDialogueDecision();
        Assert.That(decision.Run(null), Is.EqualTo(false));
    }
}
