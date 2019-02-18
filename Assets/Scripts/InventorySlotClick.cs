using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlotClick : MonoBehaviour {
    private Image itemimage;
    private InventorySystem Inventory;
    public int SlotNumber;
	// Use this for initialization
	void Start () {
        Inventory = transform.parent.parent.gameObject.GetComponent<InventorySystem>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void TappedOn()
    {

            Inventory.Itemclicked(SlotNumber);
        
    }
}
