using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.AI;

public class FiniteStateMachine : MonoBehaviour
{
    public State CurrentState;

    public NavMeshAgent Agent { get; private set; }

    public void Awake()
    {
        Agent = GetComponent<NavMeshAgent>();
    }

    public void Start()
    {
        SetDestination(transform.position);
    }

    public void Update()
    {
        CurrentState.Run(this);
    }

    public void SetDestination(Vector3 loc)
    {
        Agent.SetDestination(loc);
    }

    public void UpdateState(State state)
    {
        CurrentState = state;
    }
}