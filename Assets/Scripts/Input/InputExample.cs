using System.Collections;
using System.Collections.Generic;
using System.Text;
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

    IEnumerator StartChange(string b)
    {
        isChanging = true;
        yield return InputManager.UpdateInput(b, save: true);
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

            if (GUILayout.Button("Update \"V\""))
            {
                StartCoroutine(StartChange("Vertical"));
            }

            if (GUILayout.Button("Update \"H\""))
            {
                StartCoroutine(StartChange("Horizontal"));
            }

            if (GUILayout.Button("Update \"T\""))
            {
                StartCoroutine(StartChange("Temp"));
            }
        }
    }
}
