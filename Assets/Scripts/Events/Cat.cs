using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cat : MonoBehaviour
{
    public AudioClip sound;
    public Transform Location;

    public void StartSound()
    {
        GameObject.Find("SfxPlayer").GetComponent<SfxPlayer>().PlaySfx(sound, Location.position);
    }

    public void StopSound()
    {

    }
}
