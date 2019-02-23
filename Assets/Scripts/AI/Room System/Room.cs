using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace CreamyCheaks.AI.RoomSystem
{
    public class Room : MonoBehaviour
    {
        [SerializeField] List<Room> PossibleNextRooms = new List<Room>();
        [SerializeField] List<Transform> RoomPoints = new List<Transform>();
        public List<PointOfInterest> RoomPOIs = new List<PointOfInterest>();
        public bool IsUpstairs;
        public bool IsLocked;

        public Room GetNextRoom(Room lastRoom)
        {
            var roomManager = GameObject.Find("RoomManager").GetComponent<RoomHandler>();

            var tempList = new List<Room>();

            if (roomManager.PlayerHasBeenUpstairs)
                tempList = PossibleNextRooms.Where(room => room.IsLocked == false).ToList();
            else
                tempList = PossibleNextRooms.Where(room => room.IsUpstairs == false && room.IsLocked == false).ToList();

            if (tempList.Contains(lastRoom))
            {
                tempList.Remove(lastRoom);
            }

            int r = Random.Range(0, tempList.Count);
            return tempList[r];
        }

        public Transform GetPoint()
        {
            int r = Random.Range(0, RoomPoints.Count);
            return RoomPoints[r];
        }
    }
}