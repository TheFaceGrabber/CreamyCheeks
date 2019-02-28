using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Window : Interactable {
    public bool IsLeft;
    public AudioClip WindowSfx;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public override void PlayerInteract()
    {
       if (IsLeft) transform.Rotate(0, -90, 0);
       else transform.Rotate(0, 90, 0);
        IsLeft = !IsLeft;
        Sfx.PlaySfx(WindowSfx, transform.position);
    }
}
