using System.Collections;
using System.Collections.Generic;
using CreamyCheaks.AI;
using CreamyCheaks.DialogSystem;
using UnityEngine;

public class AIInteractor : Interactable
{
    public Branch InitBranch;

    private FiniteStateMachine fsm;

    void Start()
    {
        fsm = GetComponent<FiniteStateMachine>();
    }

    public override void PlayerInteract()
    {
        fsm.RequestTalk();
        GameObject.FindGameObjectWithTag("Player").GetComponent<DialogueHolder>().BeginDialogue(InitBranch, fsm);
    }
}
