using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FridgeDoor : Interactable
{
    public bool IsOpen;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void PlayerInteract()
    {
        if (IsOpen) transform.Rotate(0, 0, 90, Space.Self);
        else transform.Rotate(0, 0, -90, Space.Self);
        IsOpen = !IsOpen;
    }
}
