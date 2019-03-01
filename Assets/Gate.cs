using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : Interactable {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public override void PlayerInteract()
    {
        transform.Rotate(0, 90, 0);
        GetComponent<BoxCollider>().enabled = false;
        transform.position = new Vector3(-11.281f, 6, 131.726f);
    }
}
