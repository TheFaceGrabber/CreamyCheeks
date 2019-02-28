using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockedDoor : Door {
    public Item ItemNeeded;
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

            if (Inventory.CheckForItem(ItemNeeded))
            {

                base.PlayerInteract();
            }
        



    }
}
