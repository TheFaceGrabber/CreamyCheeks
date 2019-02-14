using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour {
    private GameObject InteractText;
	// Use this for initialization
	void Start () {
        InteractText = transform.GetChild(3).gameObject;



        HideInteractText();
	}
	
	// Update is called once per frame


    public void ShowInteractText()
    {
        InteractText.SetActive(true);
    }

    public void HideInteractText()
    {
        InteractText.SetActive(false);
    }


    public void ShowItemDescription()
    {

    }

    public void HideItemDescription()
    {

    }

    public void ShowStats()
    {

    }

    public void HideStats()
    {

    }

    public void SetStats()
    {

    }
}
