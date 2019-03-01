using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class toyRoomScript : Interactable {

    public GameObject player;
    private bool played;

    private void Awake()
    {
        played = false;
    }

    public void doThis()
    {
        player.GetComponent<rpgStats>().Agility.Add(-1);
        player.GetComponent<rpgStats>().Strength.Add(-1);
    }

    private void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.name == "Player")
        {
            if (!played)
            {
                GetComponent<PlayableDirector>().Play();
                played = true;
            }
        }
    }

}
