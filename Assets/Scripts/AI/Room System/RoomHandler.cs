using UnityEngine;
using System.Collections.Generic;

namespace CreamyCheaks.AI.RoomSystem
{
    public class RoomHandler : MonoBehaviour
    {
        [SerializeField] List<Room> AllRooms = new List<Room>();

        public bool PlayerHasBeenUpstairs;

        public Room FallbackRoom
        {
            get { return AllRooms[0]; }
        }

        public Room GetNextRoom(Room currentRoom, Room last)
        {
            Room cur = currentRoom != null ? currentRoom : FallbackRoom;
            Room la = last != null ? last : FallbackRoom;

            return cur.GetNextRoom(la);
        }
    }
}