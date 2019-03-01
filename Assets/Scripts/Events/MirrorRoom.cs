using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using CreamyCheaks.PlayerController;
using CreamyCheaks.AI;
using UnityEngine.AI;

public class MirrorRoom : MonoBehaviour
{
    private bool hasMadeSacrifice = false;

    public AudioClip screamSound;

    public GameObject EndGameTrigger;

    public Transform screamLocation;

    public Item Machete;

    public FiniteStateMachine[] Fsm;
    public Transform[] FsmLocations;

    public void Begin()
    {
        hasMadeSacrifice = false;
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().SetAllowInput(false);
        GameObject.FindGameObjectWithTag("Player").transform.eulerAngles = Vector3.zero;
        GameObject.Find("Inventory").GetComponent<InventorySystem>().OnDeleteFromInventory += OnOnDeleteFromInventory;

        Fsm = GameObject.FindObjectsOfType<FiniteStateMachine>();

        StartCoroutine(Go());
    }

    private void OnOnDeleteFromInventory(Item item)
    {
        Destroy(item.gameObject);
        hasMadeSacrifice = true;
    }

    IEnumerator Go()
    {
        yield return new WaitForSeconds(3.2f);

        yield return new WaitUntil(() => hasMadeSacrifice == true);

        GameObject.Find("Inventory").GetComponent<InventorySystem>().OnDeleteFromInventory -= OnOnDeleteFromInventory;

        GetComponent<PlayableDirector>().Play();

        yield return new WaitForSeconds(3.5f);

        GameObject.Find("Inventory").GetComponent<InventorySystem>().AddItem(Machete);

        yield return new WaitForSeconds(0.5f);

        GameObject.Find("SfxPlayer").GetComponent<SfxPlayer>().PlaySfx(screamSound, screamLocation.position);

        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().SetAllowInput(true);

        for (int i = 0; i < Fsm.Length; i++)
        {
            Fsm[i].enabled = false;
            Fsm[i].GetComponent<NavMeshAgent>().enabled = false;
            Fsm[i].Animator.SetBool("IsWalking", false);
            Fsm[i].transform.position = FsmLocations[i].transform.position;
            Fsm[i].transform.eulerAngles = FsmLocations[i].transform.eulerAngles;
        }

        EndGameTrigger.SetActive(true);

        //TODO ADD MACHETTE
    }

    public void EndGame()
    {
        FindObjectOfType<UIManager>().EndGame();
    }
}
