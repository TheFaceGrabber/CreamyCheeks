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
            var obj = GameObject.Find("RoomManager");

            if (obj == null)
            {
                Debug.Log("No \"RoomManager\" object found in the scene!");
                return this;
            }

            var roomManager = obj.GetComponent<RoomHandler>();

            if (roomManager == null)
            {
                Debug.Log("\"RoomManager\" object does not have a \"RoomHandler\" component attached!");
                return this;
            }

            var tempList = new List<Room>();

            if (roomManager.PlayerHasBeenUpstairs)
            {
                tempList = PossibleNextRooms.Where(room => room.IsLocked == false).ToList();
            }
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
            if (RoomPoints.Count > 0)
            {
                int r = Random.Range(0, RoomPoints.Count);
                return RoomPoints[r];
            }
            
            return null;
        }
    }
}