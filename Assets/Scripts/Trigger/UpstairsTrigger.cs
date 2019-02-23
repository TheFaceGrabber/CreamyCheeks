using System.Collections;
using System.Collections.Generic;
using CreamyCheaks.AI.RoomSystem;
using UnityEngine;

public class UpstairsTrigger : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        RoomHandler handler = GameObject.Find("RoomManager").GetComponent<RoomHandler>();
        if (handler)
        {
            handler.SetPlayerHasBeenUpstairs(true);
        }
    }

}
