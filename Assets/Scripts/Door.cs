using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Interactable {
    protected GameObject DoorOne;
    protected GameObject DoorTwo;
    protected bool DoorOpen;
    public AudioClip OpenSfx;
    
	// Use this for initialization
	void Start () {
        DoorOne = transform.parent.transform.GetChild(0).gameObject;
        DoorTwo = transform.parent.transform.GetChild(1).gameObject;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public override void PlayerInteract()
    {
        GetComponent<BoxCollider>().enabled = false;
        DoorOne.transform.Rotate(new Vector3(0, -90, 0));
        DoorTwo.transform.Rotate(new Vector3(0, 90, 0));
        Sfx.PlaySfx(OpenSfx, transform.position);
    }


}
