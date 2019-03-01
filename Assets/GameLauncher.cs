using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameLauncher : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void GameStart()
    {
        SceneManager.LoadScene(1); //tomorrow need to fade to black, bring up a loading screen, unfade to game
    }

    public void GameExit()
    {
        Application.Quit();
    }
}
