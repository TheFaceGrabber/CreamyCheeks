using System.Collections;
using System.Collections.Generic;
using CreamyCheaks.AI;
using UnityEngine;

public class ObjectBreakReactor : MonoBehaviour
{

    public void OnBreak()
    {
        RaycastHit hit;
        if (Physics.SphereCast(transform.position, 5, transform.forward, out hit, 0))
        {
            var fsm = hit.collider.gameObject.GetComponent<FiniteStateMachine>();
            if (fsm)
            {

            }
        }
    }

}
