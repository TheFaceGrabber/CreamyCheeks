using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningManager : MonoBehaviour {
    private Lightning[] Lightnings;
    public AudioClip LightningStrikeOne;
    public AudioClip LightningStrikeTwo;
    public AudioClip LightningStrikeThree;
    private SfxPlayer Sfx;
	// Use this for initialization
	void Start () {
        Lightnings = GameObject.FindObjectsOfType<Lightning>();
        Sfx = GameObject.Find("SfxPlayer").GetComponent<SfxPlayer>();
        StartCoroutine(Strike());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator Strike()
    {
        int i = Random.Range(1, 3);
        AudioClip NextPlay = null;
        switch (i)
        {
            case (1):
                NextPlay = LightningStrikeOne;
                break;
            case (2):
                NextPlay = LightningStrikeTwo;

                break;
            case (3):
                NextPlay = LightningStrikeThree;
                break;
        };
        Sfx.PlayLightningSfx(NextPlay);
        foreach (Lightning Light in Lightnings) Light.LightningStrikes();
        yield return new WaitForSeconds(NextPlay.length);
        StartCoroutine(Strike());
   }


}
