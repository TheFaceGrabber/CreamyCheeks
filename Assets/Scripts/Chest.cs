using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : Interactable {

    private bool IsOpen;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public override void PlayerInteract()
    {
        IsOpen = !IsOpen;
        if (IsOpen)
        {
            transform.Rotate(-90, 0, 0);
        }
        else
        {
            transform.Rotate(90, 0, 0);
        }
    }
}
