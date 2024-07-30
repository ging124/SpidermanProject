using UnityEditor;
using UnityEngine;


[CustomEditor(typeof(MissionManagerSO))]
public class CustomButon : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        if (GUILayout.Button("Complete Mission"))
        {
            MissionManagerSO mission = (MissionManagerSO)target;
            mission.FinishedMission();
        }
    }
}
