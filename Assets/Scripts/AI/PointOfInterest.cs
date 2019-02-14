using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace CreamyCheaks.AI
{
    public class PointOfInterest : MonoBehaviour
    {
        public bool IsInUse { get; set; }

        public float UseTime = 1.0f;

        private void OnDrawGizmos()
        {
            var c = Gizmos.color;
            Gizmos.color = Color.red;
            Gizmos.DrawCube(transform.position, Vector3.one);
            Gizmos.color = c;

            Gizmos.DrawLine(transform.position, transform.position + transform.forward);
        }
    }
}