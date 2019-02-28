using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour {
    private GameObject InfoPanel;
    private Image ItemImage;
    private Text ItemName;
    private Text ItemDesc;
    private Item currentitem;
    private InventorySystem Inventory;
    private SfxPlayer Sfx;
    public AudioClip ItemInspectedSfx;
    public AudioClip PanelCloseSfx;
    public AudioClip ItemScrapSfx;

	// Use this for initialization
	void Start () {
        Sfx = GameObject.Find("SfxPlayer").GetComponent<SfxPlayer>();
        InfoPanel = transform.GetChild(1).gameObject;
        Inventory = transform.GetChild(0).gameObject.GetComponent<InventorySystem>();
        ItemImage = InfoPanel.transform.GetChild(0).GetChild(0).gameObject.GetComponent<Image>();
        ItemName = InfoPanel.transform.GetChild(6).gameObject.GetComponent<Text>();
        ItemDesc = InfoPanel.transform.GetChild(7).gameObject.GetComponent<Text>();
        HidePanel();
       // InventoryLoad();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    

    public void HidePanel()
    {
        InfoPanel.SetActive(false);
        currentitem = null;
        Sfx.PlaySfx(PanelCloseSfx);
    }

    void EnablePanel()
    {
        InfoPanel.SetActive(true);
    }


    public void UpdateInfo(Item item)
    {
        InfoPanel.SetActive(true);
        currentitem = item;
        ItemImage.sprite = item.sprite;
        ItemName.text = item.ItemName;
        ItemDesc.text = item.ItemDescription;
        Sfx.PlaySfx(ItemInspectedSfx);

        
    }



    //public void InventorySave(GameManager GM)
    //{
    //    GM.SaveInventory(Inventory.items, Gold);
    //}

    //public void InventoryLoad()
    //{
    //    Item[] tempitems = GameObject.Find("GameManager").GetComponent<GameManager>().GetInventory();
    //    if (tempitems != null)
    //    {


    //        foreach (Item thisitem in tempitems)
    //        {
    //            if (thisitem != null)
    //            {
    //                Inventory.AddItem(thisitem);
    //            }
                
    //        }
    //    }
    //}

}
