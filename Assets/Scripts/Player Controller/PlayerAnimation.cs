using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CreamyCheaks.Input;

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
            if (InputManager.GetAxis("Move Forward") > 0)
            {
                anim.SetBool("isWalkingBack", false);
                anim.SetBool("isWalking", true);
            }
            else if(InputManager.GetAxis("Move Forward") < 0)
            {
                anim.SetBool("isWalking", false);
                anim.SetBool("isWalkingBack", true);
            }
            else
            {
                anim.SetBool("isWalking", false);
                anim.SetBool("isWalkingBack", false);
            }
        }	
	}
}
