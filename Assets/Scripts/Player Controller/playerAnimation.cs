using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerAnimation : MonoBehaviour {

    private Animator anim;
    private bool crouching;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKey(KeyCode.C))
        {
            crouching = true;
            anim.SetBool("isCrouching", true);
        }
        else
        {
            crouching = false;
            anim.SetBool("isCrouching", false);
        }

        if (Input.GetKey(KeyCode.W))
        {
            anim.SetBool("isWalking", true);
            anim.SetBool("isWalkingBack", false);
        } else if (Input.GetKey(KeyCode.S))
        {
            anim.SetBool("isWalking", false);
            anim.SetBool("isWalkingBack", true);
        } else
        {
            anim.SetBool("isWalking", false);
            anim.SetBool("isWalkingBack", false);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            anim.SetTrigger("Jump");
        }
	}
}
