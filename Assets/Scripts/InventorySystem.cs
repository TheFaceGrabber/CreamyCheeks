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
    private int SelectedItem;
    private bool LeftOption;
    private bool SelectionUp;
    private Image LeftSelection;
    private Image RightSelection;

    public const int numItemSlots = 10;

    private void Start()
    {
        Sfx = GameObject.Find("SfxPlayer").GetComponent<SfxPlayer>();
        IManager = transform.parent.gameObject.GetComponent<InventoryManager>();
        for (int i = 0; i < numItemSlots; i++)
        {
            ItemImages[i] = transform.GetChild(i).GetChild(0).GetComponent<Image>();
        }
        LeftSelection = transform.parent.GetChild(1).GetChild(1).GetComponent<Image>();
        RightSelection = transform.parent.GetChild(1).GetChild(2).GetComponent<Image>();
    }

    private void Update() //Need to add Controller input here
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            SelectLeft();
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            SelectRight();
        }

        if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            SelectUp();
        }

        if (Input.GetKeyDown(KeyCode.DownArrow) )
        {
            SelectDown();
        }
    }

    private void SelectLeft()
    {
        if (!SelectionUp)
        {
            transform.GetChild(SelectedItem).GetChild(1).GetComponent<Image>().color = Color.white;
            SelectedItem--;
            if (SelectedItem < 0)
            {
                SelectedItem = 9;
            }
            transform.GetChild(SelectedItem).GetChild(1).GetComponent<Image>().color = Color.red;
        }
        else
        {
            if (LeftOption)
            {
                LeftSelection.color = Color.white;
                RightSelection.color = Color.red;
            }
            else
            {
                LeftSelection.color = Color.red;
                RightSelection.color = Color.white;
            }
            LeftOption = !LeftOption;
        }
    }

    private void SelectRight()
    {
        if (!SelectionUp)
        {
            transform.GetChild(SelectedItem).GetChild(1).GetComponent<Image>().color = Color.white;
            SelectedItem++;
            if (SelectedItem > 9)
            {
                SelectedItem = 0;
            }
            transform.GetChild(SelectedItem).GetChild(1).GetComponent<Image>().color = Color.red;
        }
        else
        {
            if (LeftOption)
            {
                LeftSelection.color = Color.white;
                RightSelection.color = Color.red;
            }
            else
            {
                LeftSelection.color = Color.red;
                RightSelection.color = Color.white;
            }
            LeftOption = !LeftOption;
        }
    }

    private void SelectUp()
    {
        if (!SelectionUp && (items[SelectedItem] != null))
        {
            SelectionUp = !SelectionUp;
            IManager.UpdateInfo(items[SelectedItem]);
        }
    }

    private void SelectDown()
    {
        if (SelectionUp)
        {
            SelectionUp = !SelectionUp;
            IManager.HidePanel();
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
                //this removes the item -> GameObject.Find("Inventory").GetComponent<InventorySystem>().RemoveItem(<< EnterItemhere >>);
                //if want to spawn in world can Instantiate (<<Item>>, player.transform.position)
                //items can be found in assets/prefabs/world_items
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
