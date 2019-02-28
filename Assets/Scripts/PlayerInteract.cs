using System.Collections;
using System.Collections.Generic;
using CreamyCheaks.Input;
using UnityEngine;

public class PlayerInteract : MonoBehaviour {
    public GameObject FirstPersonCamera;
    public LayerMask WhatIsInteractable;
    private UIManager UI;
    private GameObject CurrentInteractObject;
    public float InteractDistance;
    public bool hasItem;

	// Use this for initialization
	void Start () {
        UI = GameObject.Find("UI").GetComponent<UIManager>();
        FirstPersonCamera = transform.GetChild(0).gameObject;
	}
	
	// Update is called once per frame
	void Update () {
        RaycastHit hit;
        Debug.DrawRay(FirstPersonCamera.transform.position, FirstPersonCamera.transform.TransformDirection(Vector3.forward), Color.red);
        if (Physics.Raycast(FirstPersonCamera.transform.position, FirstPersonCamera.transform.TransformDirection(Vector3.forward), out hit, InteractDistance, WhatIsInteractable) && !hasItem)
        {
            if (CurrentInteractObject == null || CurrentInteractObject != hit.transform.gameObject)
            {

                UI.ShowInteractText();
                CurrentInteractObject = hit.transform.gameObject;
                UI.UpdateInteractText(CurrentInteractObject.GetComponent<Interactable>().InteractText);
            }
           
        }
        else
        {
            if (CurrentInteractObject != null && !hasItem)
            {
                CurrentInteractObject = null;
                UI.HideInteractText();
            }
        }

       // if (Input.GetKeyDown(KeyCode.Z)) 
        if (InputManager.GetButtonDown("Interact"))
        {
            if (CurrentInteractObject != null)
            {
                //CurrentInteractObject = null;
                UI.HideInteractText();
                CurrentInteractObject.GetComponent<Interactable>().PlayerInteract();
                
            }
        }
	}

    public void HoldingItem(GameObject item)
    {
        hasItem = !hasItem;
        if (hasItem)
        {
            CurrentInteractObject = item;
            UI.ShowInteractText();
            UI.UpdateInteractText("to Throw");
        }
        else
        {
            CurrentInteractObject = null;
            UI.HideInteractText();
        }
    }
}
