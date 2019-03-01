using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackCandle : Item {

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

    public override void ItemUsed() //when player clicks use on item fill info here
    {
       
    }
}
