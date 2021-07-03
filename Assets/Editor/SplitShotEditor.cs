using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor (typeof(SplitShot))]
public class SplitShotEditor : Editor
{
    private void OnSceneGUI()
    {
        SplitShot ss = (SplitShot)target;
        Handles.color = Color.white;
        Handles.DrawWireArc(ss.transform.position, Vector3.up, Vector3.forward, 360, ss.viewRadius);
        Vector3 viewAngleA = ss.DirFromAngle(-ss.viewAngle / 2, false);
        Vector3 viewAngleB = ss.DirFromAngle(ss.viewAngle / 2, false);

        Handles.DrawLine(ss.transform.position, ss.transform.position + viewAngleA * ss.viewRadius);
        Handles.DrawLine(ss.transform.position, ss.transform.position + viewAngleB * ss.viewRadius);
    }
}
