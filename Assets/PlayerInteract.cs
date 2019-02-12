using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour {
    public GameObject FirstPersonCamera;
    public LayerMask WhatIsInteractable;
    private UIManager UI;
    private GameObject CurrentInteractObject;
    public float InteractDistance;

	// Use this for initialization
	void Start () {
        UI = GameObject.Find("UI").GetComponent<UIManager>();
        FirstPersonCamera = transform.GetChild(0).gameObject;
	}
	
	// Update is called once per frame
	void Update () {
        RaycastHit hit;

        if (Physics.Raycast(FirstPersonCamera.transform.position, FirstPersonCamera.transform.TransformDirection(Vector3.forward), out hit, InteractDistance, WhatIsInteractable))
        {
            if (CurrentInteractObject == null)
            {
                UI.ShowInteractText();
                CurrentInteractObject = hit.transform.gameObject;
            }
           
        }
        else
        {
            if (CurrentInteractObject != null)
            {
                CurrentInteractObject = null;
                UI.HideInteractText();
            }
            
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (CurrentInteractObject != null)
            {
                CurrentInteractObject.GetComponent<Interactable>().PlayerInteract();
                CurrentInteractObject = null;
                UI.HideInteractText();
            }
        }
	}
}
