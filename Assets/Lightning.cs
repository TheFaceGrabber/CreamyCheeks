using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lightning : MonoBehaviour {
    private Light SpotLight;
	// Use this for initialization
	void Awake () {
        SpotLight = GetComponent<Light>();
        SpotLight.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void LightningStrikes()
    {
        StartCoroutine(LightningStrike());
    }
   public IEnumerator LightningStrike()
    {
        yield return new WaitForSeconds(0.5f);
        SpotLight.enabled = true;
        yield return new WaitForSeconds(0.2f);
        SpotLight.enabled = false;
        yield return new WaitForSeconds(0.1f);
        SpotLight.enabled = true;
        yield return new WaitForSeconds(0.1f);
        SpotLight.enabled = false;
        SpotLight.enabled = true;
        yield return new WaitForSeconds(0.2f);
        SpotLight.enabled = false;
        yield return new WaitForSeconds(0.1f);
        SpotLight.enabled = true;
        yield return new WaitForSeconds(0.1f);
        SpotLight.enabled = false;
       // StartCoroutine(LightningStrike());
    }
}
