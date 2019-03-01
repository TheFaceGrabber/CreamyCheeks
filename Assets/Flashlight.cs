using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : Item
{




    public override void PlayerInteract()
    {
        if (ItemPickedUp())
        {
            this.gameObject.SetActive(false);
        }
    }
}
