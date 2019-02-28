using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : Interactable {

    public Sprite sprite;
    public string ItemName;
    public string ItemDescription;
    public int Value;
    public GameObject WorldItem;
    private InventorySystem Inventory;
	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    public bool ItemPickedUp()
    {
        Inventory = GameObject.Find("Inventory").GetComponent<InventorySystem>();
        return Inventory.AddItem(this);
    }

    public virtual void ItemUsed()
    {

    }



}
