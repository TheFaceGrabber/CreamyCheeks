using UnityEngine;
using System.Collections.Generic;

namespace CreamyCheaks.AI.RoomSystem
{
    public class Room : MonoBehaviour
    {
        [SerializeField] List<Room> PossibleNextRooms = new List<Room>();
        [SerializeField] List<Transform> RoomPoints = new List<Transform>();
        public List<PointOfInterest> RoomPOIs = new List<PointOfInterest>();

        public Room GetNextRoom(Room lastRoom)
        {
            var tempList = PossibleNextRooms;

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