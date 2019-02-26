using System.Collections;
using System.Collections.Generic;
using CreamyCheaks.AI.Actions;
using CreamyCheaks.AI.Decisions;
using CreamyCheaks.AI.RoomSystem;
using UnityEngine;
using NUnit.Framework;

public class RoomSystemTests
{
    [Test]
    public void RoomGetPointTest()
    {
        Room room = new Room();
        room.GetPoint();
    }

    [Test]
    public void RoomGetNextRoomTest()
    {
        Room room = new Room();
        room.GetNextRoom(null);
    }

    [Test]
    public void RoomManagerTest()
    {
        RoomHandler room = new RoomHandler();
        room.GetNextRoom(null, null);
    }

}
