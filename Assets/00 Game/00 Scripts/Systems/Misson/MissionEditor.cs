using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(BaseMission))]
public class MissionEditor : Editor
{
    private enum DisplayCategory { Fighting, StopCar, Shipping }
    private DisplayCategory categoryToDisplay;

    // Ghi đè phương thức OnInspectorGUI để tùy chỉnh inspector
    public override void OnInspectorGUI()
    {
        // Vẽ menu thả xuống
        categoryToDisplay = (DisplayCategory)EditorGUILayout.EnumPopup("Display", categoryToDisplay);
        EditorGUILayout.Space();

        // Vẽ các thuộc tính dựa trên danh mục đã chọn
        SerializedProperty basicInfo = serializedObject.FindProperty("basicInfo");
        SerializedProperty combatInfo = serializedObject.FindProperty("combatInfo");
        SerializedProperty magicInfo = serializedObject.FindProperty("magicInfo");

        switch (categoryToDisplay)
        {
            case DisplayCategory.Fighting:
                EditorGUILayout.PropertyField(basicInfo, new GUIContent("Basic Info"));
                break;
            case DisplayCategory.StopCar:
                EditorGUILayout.PropertyField(combatInfo, new GUIContent("Combat Info"));
                break;
            case DisplayCategory.Shipping:
                EditorGUILayout.PropertyField(magicInfo, new GUIContent("Magic Info"));
                break;
        }

        // Áp dụng các thay đổi
        serializedObject.ApplyModifiedProperties();
    }
}
