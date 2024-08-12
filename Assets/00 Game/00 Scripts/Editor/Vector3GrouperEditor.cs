using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(WallScript))]
public class Vector3GrouperEditor : Editor
{
    public override void OnInspectorGUI()
    {
        // Hiển thị mặc định cho các thuộc tính khác
        DrawDefaultInspector();

        // Lấy đối tượng đang được chỉnh sửa
        WallScript grouper = (WallScript)target;
        // Hiển thị các groupedVectors
        if (grouper.groupedVectors != null && grouper.groupedVectors.Count > 0)
        {
            EditorGUILayout.LabelField("Grouped Vectors", EditorStyles.boldLabel);
            foreach (var group in grouper.groupedVectors)
            {
                EditorGUILayout.BeginVertical("box");
                EditorGUILayout.LabelField($"Height: {group[0].y}");

                foreach (var vec in group)
                {
                    EditorGUILayout.Vector3Field("Vector", vec);
                }

                EditorGUILayout.EndVertical();
            }
        }
    }
}