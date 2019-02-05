using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using CreamyCheaks.Input;
using UnityEngine;
using UnityEditor;

namespace CreamyCheaks.Input
{
    [CustomEditor(typeof(InputDefaults))]
    public class InputDefaultsEditor : Editor
    {
        List<bool?> visibleInputs = new List<bool?>();

        public override void OnInspectorGUI()
        {
            var targ = (InputDefaults) target;
            
            if (GUILayout.Button("+"))
            {
                targ.Inputs.Add(new Button() { Name = "New Button" });
            }

            for (int i = 0; i < targ.Inputs.Count; i++)
            {
                if(visibleInputs.ElementAtOrDefault(i) == null)
                    visibleInputs.Add(false);

                visibleInputs[i] = EditorGUILayout.Foldout(visibleInputs[i].Value, targ.Inputs[i].Name);
                if (visibleInputs[i].Value)
                {
                    EditorGUI.indentLevel = 1;

                    targ.Inputs[i].Name = EditorGUILayout.TextField("Input Name", targ.Inputs[i].Name);

                    EditorGUILayout.LabelField("Main Input...", EditorStyles.boldLabel);

                    targ.Inputs[i].MainInputType =
                        (ButtonInputType)EditorGUILayout.EnumPopup("Main Input Type",targ.Inputs[i].MainInputType);

                    if (targ.Inputs[i].MainInputType == ButtonInputType.Keys)
                    {
                        targ.Inputs[i].PositiveKey = (KeyCode)EditorGUILayout.EnumPopup("Positive/Main Key", targ.Inputs[i].PositiveKey);
                        targ.Inputs[i].NegativeKey = (KeyCode)EditorGUILayout.EnumPopup("Negative Key", targ.Inputs[i].NegativeKey);
                    }
                    else
                    {
                        targ.Inputs[i].Axis = EditorGUILayout.TextField("Axis", targ.Inputs[i].Axis);
                        targ.Inputs[i].ButtonDirection = (PositiveNegative)EditorGUILayout.EnumPopup("Axis Direction", targ.Inputs[i].ButtonDirection);
                    }

                    EditorGUILayout.LabelField("Alt Input...", EditorStyles.boldLabel);

                    targ.Inputs[i].AlternativeInputType =
                        (ButtonInputType)EditorGUILayout.EnumPopup("Alt Input Type", targ.Inputs[i].AlternativeInputType);


                    if (targ.Inputs[i].AlternativeInputType == ButtonInputType.Keys)
                    {
                        targ.Inputs[i].AlternativePositiveKey = (KeyCode)EditorGUILayout.EnumPopup("Positive/Main Key", targ.Inputs[i].AlternativePositiveKey);
                        targ.Inputs[i].AlternativeNegativeKey = (KeyCode)EditorGUILayout.EnumPopup("Negative Key", targ.Inputs[i].AlternativeNegativeKey);
                    }
                    else
                    {
                        targ.Inputs[i].AlternativeAxis = EditorGUILayout.TextField("Axis", targ.Inputs[i].AlternativeAxis);
                        targ.Inputs[i].AlternativeButtonDirection = (PositiveNegative)EditorGUILayout.EnumPopup("Axis Direction", targ.Inputs[i].AlternativeButtonDirection);
                    }
                    
                    if (GUILayout.Button("-"))
                    {
                        targ.Inputs.RemoveAt(i);
                        visibleInputs.RemoveAt(i);
                    }
                    EditorGUI.indentLevel = 0;
                }
            }

            if(GUI.changed)
                EditorUtility.SetDirty(targ);
        }
    }
}
