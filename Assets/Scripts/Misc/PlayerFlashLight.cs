using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CreamyCheaks.Input;

public class PlayerFlashLight : MonoBehaviour {

    public Item RequiredItem;

    InventorySystem inv;
    Light light;
    
    private void Start()
    {
        inv = GameObject.Find("Inventory").GetComponent<InventorySystem>();
        light = GetComponent<Light>();
    }

    void Update ()
    {
		if(InputManager.GetButtonDown("Toggle Flashlight"))
        {
            if(inv.CheckForItem(RequiredItem))
            {
                light.enabled = !light.enabled;
            }
        }
	}
}
