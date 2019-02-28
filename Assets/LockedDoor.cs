using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CreamyCheaks.AI.RoomSystem;
using System.Linq;

public class LockedDoor : Door {
    public Item ItemNeeded;

    [Tooltip("Use the gameObject name of the room object")]
    public string RoomName;

    Room Room;
    private InventorySystem Inventory;
	// Use this for initialization
	void Start () {
        Inventory = GameObject.Find("Inventory").GetComponent<InventorySystem>();
        DoorOne = transform.parent.transform.GetChild(0).gameObject;
        DoorTwo = transform.parent.transform.GetChild(1).gameObject;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public override void PlayerInteract()
    {
        if(Room == null)
            Room = GameObject.Find("RoomManager").GetComponent<RoomHandler>().AllRooms.SingleOrDefault(x => x.name == RoomName);

        if (Inventory.CheckForItem(ItemNeeded) || !Room.IsLocked)
        {
            Room.IsLocked = false;
            base.PlayerInteract();
        }
    }
}
