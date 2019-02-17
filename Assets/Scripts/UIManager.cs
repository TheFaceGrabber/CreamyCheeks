using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {
    private GameObject InteractText;
    private GameObject StatsPanel;
	// Use this for initialization
	void Start () {
        InteractText = transform.GetChild(3).gameObject;
        StatsPanel = transform.GetChild(1).gameObject;


        HideInteractText();
	}

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            StatsPanel.SetActive(!StatsPanel.activeInHierarchy);
        }
    }

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

    public void SetStats(int strength, int agility, int intelligence, int willpower, int perception, int charisma)
    {
        int[] values = new int[6];
        values[0] = strength;
        values[1] = agility;
        values[2] = intelligence;
        values[3] = willpower;
        values[4] = perception;
        values[5] = charisma;
        for (int i = 0; i < 6; i++)
        {
            for (int x = 0; x < values [i]; x++)
            {
                transform.GetChild(1).GetChild(i).GetChild(x).GetComponent<Image>().color = Color.yellow;
            }
        }
    }
}
