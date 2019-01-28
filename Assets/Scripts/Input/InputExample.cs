using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CreamyCheaks.Input;

public class InputExample : MonoBehaviour
{
    private float v;
    private float h;
    private bool t;

    private bool isChanging;

    void Awake()
    {
        InputManager.Initialise(true);
    }

	void Update ()
	{
	    if (!isChanging)
	    {
	        v = InputManager.GetAxis("Vertical");
	        h = InputManager.GetAxis("Horizontal");

	        t = InputManager.GetButton("Temp");
	    }
	}

    IEnumerator StartChange()
    {
        isChanging = true;
        yield return InputManager.UpdateInput("Temp", save: true);
        isChanging = false;
    }

    void OnGUI()
    {
        if (isChanging)
        {
            GUILayout.Label("Running update...");
        }
        else
        {
            GUILayout.Label("V: " + v);
            GUILayout.Label("H: " + h);
            GUILayout.Label("T: " + t);

            if (GUILayout.Button("Update \"Temp\""))
            {
                StartCoroutine(StartChange());
            }
        }
    }
}
