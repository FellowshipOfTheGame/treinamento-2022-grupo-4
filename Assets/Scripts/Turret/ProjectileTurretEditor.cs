using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ProjectileTurret))]
public class ProjectileTurretEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        BaseTurret script = (BaseTurret)target;
        if(GUILayout.Button("Force Upgrade"))
        {
            script.Upgrade();
        }
    }
}
