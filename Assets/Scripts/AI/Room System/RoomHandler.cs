using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace CreamyCheaks.AI.RoomSystem
{
    public class RoomHandler : MonoBehaviour
    {
        public List<Room> AllRooms = new List<Room>();

        public bool PlayerHasBeenUpstairs;

        public void SetPlayerHasBeenUpstairs(bool t)
        {
            PlayerHasBeenUpstairs = t;
        }

        public Room FallbackRoom
        {
            get
            {
                return AllRooms.FirstOrDefault();
            }
        }

        public Room GetNextRoom(Room currentRoom, Room last)
        {
            if ((currentRoom != null || FallbackRoom != null) || (last != null || FallbackRoom != null))
            {
                Room cur = currentRoom != null ? currentRoom : FallbackRoom;
                Room la = last != null ? last : FallbackRoom;

                return cur.GetNextRoom(la);
            }

            return null;
        }
    }
}