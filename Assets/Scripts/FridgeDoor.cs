using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FridgeDoor : Interactable
{
    public bool IsOpen;
    private bool hasBeenOpened;
    public AudioClip OpenSfx;
    public string CloseText;
    public string OpenText;


    // Use this for initialization
    void Start()
    {
        hasBeenOpened = false;
        InteractText = OpenText;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void PlayerInteract()
    {
        Sfx.PlaySfx(OpenSfx, transform.position);
        if (hasBeenOpened)
        {
            if (IsOpen)
            {
                transform.Rotate(0, 0, 90, Space.Self);
                InteractText = OpenText;
            }
            else
            {
                transform.Rotate(0, 0, -90, Space.Self);
                InteractText = CloseText;
            }
            IsOpen = !IsOpen;
        } else
        {
            GetComponent<TimelineController>().Play();
            hasBeenOpened = true;
        }
    }
}
