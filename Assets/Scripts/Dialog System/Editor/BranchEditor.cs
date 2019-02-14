using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace CreamyCheaks.DialogSystem
{
    //[CustomEditor(typeof(Branch))]
    public class BranchEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            Branch b = (Branch)target;

            EditorGUILayout.LabelField("Branch Text");
            EditorGUILayout.LabelField("Lines:");
            for (var index = 0; index < b.Text.Count; index++)
            {
                EditorGUILayout.BeginHorizontal();
                b.Text[index] = EditorGUILayout.TextArea(b.Text[index]);

                if (GUILayout.Button("-", EditorStyles.miniButton))
                {
                    b.Text.RemoveAt(index);
                }

                EditorGUILayout.EndHorizontal();
            }

            if (GUILayout.Button("+", EditorStyles.miniButton))
            {
                b.Text.Add("New line...");
            }

            foreach (Branch.Reply r in b.GetReplies().ToArray())
            {
                int length = 50;

                string txt = string.IsNullOrEmpty(r.Text) ? "New Reply" :
                    r.Text.Length < length ? r.Text : (r.Text.Substring(0, length-3) + "...");
                r.FoldOut = EditorGUILayout.Foldout(r.FoldOut, txt, true);
                if(r.FoldOut)
                {
                    EditorGUI.indentLevel = 1;

                    r.Text = EditorGUILayout.TextArea(r.Text);

                    r.NextBranch = (Branch)EditorGUILayout.ObjectField(new GUIContent("Next Branch"), r.NextBranch, typeof(Branch), false);

                    if (GUILayout.Button("-"))
                        b.GetReplies().Remove(r);

                    EditorGUI.indentLevel = 0;
                }
            }

            if (b.GetReplies().Count >= 3)
                GUI.enabled = false;

            if(GUILayout.Button("Add Reply"))
            {
                try
                {
                    b.AddReply(new Branch.Reply());
                }
                catch(System.Exception ex)
                {
                    EditorUtility.DisplayDialog("Error!", ex.Message, "Okay");
                }
            }
            GUI.enabled = true;

            if (GUI.changed)
            {
                EditorUtility.SetDirty(b);
            }
        }
    }
}
