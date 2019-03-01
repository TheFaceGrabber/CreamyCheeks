using System.Collections;
using System.Collections.Generic;
using CreamyCheaks.AI;
using CreamyCheaks.DialogSystem;
using UnityEngine;

public class AIInteractor : Interactable
{
    public Branch InitBranch;

    private FiniteStateMachine fsm;
    private Branch tempBranch;

    void Awake()
    {
        fsm = GetComponent<FiniteStateMachine>();
    }

    public override void PlayerInteract()
    {
        if(fsm == null)
            fsm = GetComponent<FiniteStateMachine>();

        if(fsm != null)
            fsm.RequestTalk();

        GameObject.FindGameObjectWithTag("Player").GetComponent<DialogueHolder>().BeginDialogue(tempBranch != null ? tempBranch : InitBranch, fsm);
        tempBranch = null;
    }

    public void ForcePlayerInteraction(Branch branch = null)
    {
        tempBranch = branch;
        PlayerInteract();
    }
}
