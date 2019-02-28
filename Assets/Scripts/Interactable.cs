using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour {
    protected SfxPlayer Sfx;
	// Use this for initialization
	void Awake () {
        Sfx = GameObject.Find("SfxPlayer").GetComponent<SfxPlayer>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public virtual void PlayerInteract()
    {
        
    }
}
