using System.Collections;
using System.Collections.Generic;
using CreamyCheaks.AI.Actions;
using UnityEngine;
using NUnit.Framework;

public class ActionTests
{
    [Test]
    public void WanderTest()
    {
        WanderAction action = new WanderAction();
        action.Run(null);
    }

    [Test]
    public void DialogueTest()
    {
        DialogueAction action = new DialogueAction();
        action.Run(null);
    }

    [Test]
    public void POITest()
    {
        POIAction action = new POIAction();
        action.Run(null);
    }
}
