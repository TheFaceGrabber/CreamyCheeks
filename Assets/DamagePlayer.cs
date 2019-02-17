using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePlayer : MonoBehaviour {

    public GameObject player;
    private rpgStats playerStats;

    private void Awake()
    {
        playerStats = player.GetComponent<rpgStats>();
    }

	public void attackPlayer()
    {
        if (!playerStats.Strength.RollCheck())
        {
            playerStats.Health.Add(-1);
        }

        Debug.Log("attackPlayer executed");

    }
}
