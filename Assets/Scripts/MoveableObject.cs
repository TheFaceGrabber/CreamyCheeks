using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveableObject : Interactable {
    private bool BeingMoved;
    public float ThrowForce;
    public Vector3 rotationoffset;
    private GameObject PlayerCarryMarker;
    private PlayerInteract player;
    public AudioClip PickUpSfx;
    public AudioClip ThrowSfx;
	// Use this for initialization
	void Start () {
        SetMarker();
	}

    public void SetMarker()
    {
        PlayerCarryMarker = GameObject.Find("CarryObject");
        player = PlayerCarryMarker.transform.parent.parent.GetComponent<PlayerInteract>();
    }
	
	// Update is called once per frame
	void Update () {
		if (BeingMoved)
        {
            transform.position = PlayerCarryMarker.transform.position;
            transform.localRotation = PlayerCarryMarker.transform.parent.rotation;
            transform.Rotate(rotationoffset);
            
        }
	}

    public override void PlayerInteract()
    {
        Sfx.PlaySfx(PickUpSfx);
        BeingMoved = !BeingMoved;
        player.HoldingItem(this.gameObject);
        GetComponent<BoxCollider>().isTrigger = !GetComponent<BoxCollider>().isTrigger;
        if (!BeingMoved)
        {
            if (GetComponent<Rigidbody>())
            {
                GetComponent<Rigidbody>().AddForce(PlayerCarryMarker.transform.parent.TransformDirection(Vector3.forward) * ThrowForce, ForceMode.VelocityChange);
            }
            else
            {
                print("NoRigid");
            }
        }

        
    }
}
