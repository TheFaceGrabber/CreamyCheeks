﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Medals : Item {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public override void PlayerInteract()
    {
        if (ItemPickedUp())
        {
            this.gameObject.SetActive(false);
        }
    }
}
