using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CreamyCheaks.AI.RoomSystem;
using CreamyCheaks.DialogSystem;
using UnityEngine;
using UnityEngine.AI;

namespace CreamyCheaks.AI
{
    public class FiniteStateMachine : MonoBehaviour
    {
        public State CurrentState;

        public float RotationSpeed;

        public float DistanceLeft;

        public Quaternion WantedRotation { get; set; }

        public NavMeshAgent Agent { get; private set; }
        public Animator Animator { get; private set; }

        public Room LastRoom;
        public Room CurrentRoom;

        public PointOfInterest CurrentPOI;
        public bool IsAtPOI = false;

        public AudioClip[] ReactionsToBrokenObjects;

        public Vector3 HeadLookTarget { get; set; }

        public float InStateForSeconds { get; set; }

        public bool DoesPlayerWantToInteract { get; private set; }

        public void Awake()
        {
            Agent = GetComponent<NavMeshAgent>();
            Animator = GetComponent<Animator>();
        }

        public void Start()
        {
            SetDestination(transform.position);
        }

        public void Update()
        {
            CurrentState.Run(this);

            DistanceLeft = Vector3.Distance(Agent.destination, transform.position);

            InStateForSeconds += Time.deltaTime;

            Agent.updateRotation = false;
            transform.rotation = Quaternion.Slerp(transform.rotation, WantedRotation, RotationSpeed * Time.deltaTime);

            CurrentState.RunEndConditions(this);
        }

        public void SetDestination(Vector3 loc)
        {
            Agent.SetDestination(loc);
        }

        public void UpdateState(State state)
        {
            if(state != null)
                CurrentState = state;
        }

        private void OnAnimatorIK(int layerIndex)
        {
            if (HeadLookTarget != Vector3.zero)
            {
                Animator.SetLookAtWeight(0.9f, 0.25f, 1, 1);
                Animator.SetLookAtPosition(HeadLookTarget);
            }
        }

        public void RequestTalk()
        {
            DoesPlayerWantToInteract = true;
        }

        public void EndTalk()
        {
            DoesPlayerWantToInteract = false;
        }

        public void ReactToBreak()
        {
            int r = UnityEngine.Random.Range(0, ReactionsToBrokenObjects.Length);
            AudioClip a = ReactionsToBrokenObjects[r];
            //TODO PLAY AUDIO
        }
    }
}