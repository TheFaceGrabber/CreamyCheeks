using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurtainMain : MonoBehaviour {
    private GameObject CurtainClosed;
    private GameObject CurtainOpenLeft;
    private GameObject CurtainOpenRight;
	// Use this for initialization
	void Start () {
        CurtainClosed = transform.GetChild(1).gameObject;
        CurtainOpenLeft = transform.GetChild(2).gameObject;
        CurtainOpenRight = transform.GetChild(3).gameObject;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ToggleCurtains()
    {
        CurtainClosed.SetActive(!CurtainClosed.activeInHierarchy);
        CurtainOpenLeft.SetActive(!CurtainOpenLeft.activeInHierarchy);
        CurtainOpenRight.SetActive(!CurtainOpenRight.activeInHierarchy);
        

    }
}
