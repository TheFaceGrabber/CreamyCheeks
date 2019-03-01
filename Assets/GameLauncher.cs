using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameLauncher : MonoBehaviour {
    public Image Fader;
   
	// Use this for initialization
	void Start () {
        Fader.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void GameStart()
    {
        Fader.enabled = true;
        StartCoroutine(LoadGame());
    }

    public void GameExit()
    {
        Application.Quit();
    }

    IEnumerator LoadGame()
    {
        Color newcolor = Fader.color;
        for (int i = 0; i < 20; i++)
        {
            yield return new WaitForSeconds(0.05f);
            newcolor.a += 0.05f;
            Fader.color = newcolor;
        }

        yield return new WaitForSeconds(0);
        SceneManager.LoadScene(1);
    }
}
