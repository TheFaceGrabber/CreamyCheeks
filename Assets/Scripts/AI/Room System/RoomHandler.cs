using UnityEngine;
using System.Collections.Generic;

namespace CreamyCheaks.AI.RoomSystem
{
    public class RoomHandler : MonoBehaviour
    {
        [SerializeField] List<Room> AllRooms = new List<Room>();

        public Room GetNextRoom(Room currentRoom, Room last)
        {
            return currentRoom.GetNextRoom(last);
        }
    }
}