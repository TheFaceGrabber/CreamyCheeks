using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class startToyCinematic : Interactable {

    bool cinPlayed;

	// Use this for initialization
	void Start () {
        cinPlayed = false;
	}

    public override void PlayerInteract()
    {
        GetComponent<TimelineController>().Play();
    }
}
