using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(ViconMixedRealityCalibration))]
public class CustomButtonForCalibration : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        ViconMixedRealityCalibration myScript = (ViconMixedRealityCalibration)target;
        if (GUILayout.Button("Calibrate"))
        {
            myScript.Calibrate();
        }
    }
}
