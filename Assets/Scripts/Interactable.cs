using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour {
    protected SfxPlayer Sfx;
    //protected UIManager UI;
    public string InteractText;
	// Use this for initialization
	void Awake () {
        Sfx = GameObject.Find("SfxPlayer").GetComponent<SfxPlayer>();
    //    UI = GameObject.Find("UI").GetComponent<UIManager>();
    if (InteractText == "")
        {
            InteractText = "to Interact";
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public virtual void PlayerInteract()
    {
        
    }
}
