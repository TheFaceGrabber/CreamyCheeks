using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SfxPlayer : MonoBehaviour
{
    private GameObject Player;
    private bool IsMuted;
    private AudioSource[] SfxPlayers;
    private int LoopId;
    // Start is called before the first frame update
    void Awake()
    {
        LoopId = 99;
        Player = GameObject.Find("Player");
        SfxPlayers = transform.GetComponentsInChildren<AudioSource>();
    }

    // Update is called once per frame

    /// <summary>
    /// Feed in an audio clip to be played from SfxPlayer
    /// </summary>
    /// <param name="Sfx"></param>
    public void PlaySfx(AudioClip Sfx)
    {
        for (int i = 0; i < SfxPlayers.Length -1; i++)
        {
            if (!SfxPlayers[i].isPlaying)
            {
                SfxPlayers[i].clip = Sfx;
                SfxPlayers[i].Play();
                SfxPlayers[i].gameObject.transform.position = Player.transform.position;
                return;
            }
        }
    }

    public void PlaySfx(AudioClip Sfx, Vector3 Pos)
    {
        for (int i = 0; i < SfxPlayers.Length -1; i++)
        {
            if (!SfxPlayers[i].isPlaying)
            {
                SfxPlayers[i].gameObject.transform.position = Pos;
                SfxPlayers[i].clip = Sfx;
                SfxPlayers[i].Play();
                return;
            }
        }
    }
    public void PlayLightningSfx(AudioClip Sfx)
    {
        SfxPlayers[SfxPlayers.Length - 1].clip = Sfx;
        SfxPlayers[SfxPlayers.Length - 1].Play();
    }
    /// <summary>
    /// toggle Sfx on and off
    /// </summary>
    public void ToggleSfx()
    {
        IsMuted = !IsMuted;
        foreach (AudioSource sfx in SfxPlayers) sfx.mute = IsMuted;
    }

    public void PlayLoop(AudioClip Sfx, Vector3 Pos)
    {
        if (LoopId != 99) return;
        for (int i = 0; i < SfxPlayers.Length - 1; i++)
        {
            if (!SfxPlayers[i].isPlaying)
            {
                SfxPlayers[i].gameObject.transform.position = Pos;
                SfxPlayers[i].clip = Sfx;
                SfxPlayers[i].loop = true;
                SfxPlayers[i].Play();
                LoopId = i;
                return;
            }
        }
    }

    public void StopLoop()
    {
        if (LoopId == 99) return;
        SfxPlayers[LoopId].loop = false;
        SfxPlayers[LoopId].Stop();
        LoopId = 99;
    }

}
