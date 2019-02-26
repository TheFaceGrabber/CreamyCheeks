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

        float v = InputManager.GetAxis("Move Forward");
        float h = InputManager.GetAxis("Move Sideways");

        anim.SetFloat("V",v);
        anim.SetFloat("H",h);
	}
}
