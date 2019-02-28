using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtPlayer : MonoBehaviour {

    public Camera Cam;
	
	// Update is called once per frame
	void Update () {

        Vector3 screenPoint = Cam.WorldToViewportPoint(transform.position);
        if(!(screenPoint.z > 0 && screenPoint.x > -0.2 && screenPoint.x < 1.2 && screenPoint.y > -0.2 && screenPoint.y < 1.2))
        {
           // Debug.Log("not looking at statue");
            transform.LookAt(Cam.transform.position);
        }

	}
}
