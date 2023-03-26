using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Test))]
public class TestEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        Test t = target as Test;

        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Å×½ºÆ®"))
        {
            //t.PrintMsg();
        }
        GUILayout.EndHorizontal();
    }
}
