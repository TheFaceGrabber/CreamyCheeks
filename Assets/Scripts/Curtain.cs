using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Curtain : Interactable {
    private CurtainMain mainCurtain;
    public AudioClip CurtainSfx;
	// Use this for initialization
	void Start () {
        mainCurtain = transform.parent.GetComponent<CurtainMain>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public override void PlayerInteract()
    {
        mainCurtain.ToggleCurtains();
        Sfx.PlaySfx(CurtainSfx, transform.position);
    }


}
