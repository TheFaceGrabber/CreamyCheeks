using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CreamyCheaks.Input
{
    [CreateAssetMenu(fileName = "Input Defaults", menuName = "Creamy Cheeks/Input Defaults")]
    public class InputDefaults : ScriptableObject
    {
        public List<Button> Inputs = new List<Button>();
    }
}