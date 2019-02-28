using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySystem : MonoBehaviour {
    private InventoryManager IManager;
    public Image[] ItemImages = new Image[numItemSlots];
    public Item[] items = new Item[numItemSlots];
    public Color MyDefaultColor;
    private SfxPlayer Sfx;
    public AudioClip ItemRemovedSfx;
    public AudioClip ItemAddedSfx;

    public const int numItemSlots = 10;

    private void Start()
    {
        Sfx = GameObject.Find("SfxPlayer").GetComponent<SfxPlayer>();
        IManager = transform.parent.gameObject.GetComponent<InventoryManager>();
        for (int i = 0; i < numItemSlots; i++)
        {
            ItemImages[i] = transform.GetChild(i).GetChild(0).GetComponent<Image>();
        }
        
    }

    public bool AddItem(Item itemToAdd)
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i] == null)
            {
                items[i] = itemToAdd;
                ItemImages[i].sprite = itemToAdd.sprite;
                ItemImages[i].color = Color.white;
                Sfx.PlaySfx(ItemAddedSfx);
                return true;
            }
        }
        return false;
    }

    public bool CheckForItem(Item ItemToCheck)
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i] == null) return false;
            if (items[i].ItemName == ItemToCheck.ItemName)
            {
                return true;
            }
        }
        return false;
    }

    public void RemoveItem(Item itemToRemove)
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i] == itemToRemove)
            {
                items[i] = null;
                ItemImages[i].sprite = null;
                ItemImages[i].color = MyDefaultColor;
                Sfx.PlaySfx(ItemRemovedSfx);
                return;
            }
        }
    }


    public void Itemclicked(int SlotNumber)
    {
        if (items[SlotNumber - 1] != null)
        {


            IManager.UpdateInfo(items[SlotNumber - 1]);
        }
    }

}
