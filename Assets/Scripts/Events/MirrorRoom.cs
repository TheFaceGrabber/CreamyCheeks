using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class MirrorRoom : MonoBehaviour
{
    private bool hasMadeSacrifice = false;

    public void Begin()
    {
        hasMadeSacrifice = false;
        GameObject.Find("Inventory").GetComponent<InventorySystem>().OnDeleteFromInventory += OnOnDeleteFromInventory;

        StartCoroutine(Go());
    }

    private void OnOnDeleteFromInventory(Item item)
    {
        Destroy(item.gameObject);
        hasMadeSacrifice = true;
    }

    IEnumerator Go()
    {
        yield return new WaitForSeconds(3.2f);

        yield return new WaitUntil(() => hasMadeSacrifice == true);

        GetComponent<PlayableDirector>().Play();

        yield return new WaitForSeconds(4f);

        //TODO ADD MACHETTE
    }
}
