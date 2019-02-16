using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour {

    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKey(KeyCode.C))
        {
            anim.SetBool("isCrouching", true);
        } else
        {
            anim.SetBool("isCrouching", false);
        }

        if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S))
        {
            if (Input.GetKey(KeyCode.W))
            {
                anim.SetBool("isWalkingBack", false);
                anim.SetBool("isWalking", true);
            }

            if (Input.GetKey(KeyCode.S))
            {
                anim.SetBool("isWalking", false);
                anim.SetBool("isWalkingBack", true);
            }
        } else
        {
            anim.SetBool("isWalking", false);
            anim.SetBool("isWalkingBack", false);
        }
        	
	}
}
