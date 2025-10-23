// Editor/InteractionObjectEditor.cs
using System;
using System.Linq;
using Feature.Interaction;
using ScriptableObjects.Action;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(InteractionObject))]
public class InteractionObjectEditor : UnityEditor.Editor
{
    public override void OnInspectorGUI()
    {
        // 변경점 1: serializedObject를 항상 업데이트합니다.
        serializedObject.Update();

        // 변경점 2: base.OnInspectorGUI() 대신 DrawPropertiesExcluding 사용
        // "_actions"라는 이름의 프로퍼티를 '제외하고' 나머지 모든 것을 기본 인스펙터처럼 그려줍니다.
        DrawPropertiesExcluding(serializedObject, "_actions");

        EditorGUILayout.Space();

        var actionsProp = serializedObject.FindProperty("_actions");

        EditorGUILayout.LabelField("Actions", EditorStyles.boldLabel);

        // 이 아래 로직은 동일합니다.
        for (int i = 0; i < actionsProp.arraySize; i++)
        {
            EditorGUILayout.BeginVertical(EditorStyles.helpBox); // 각 항목을 박스로 감싸서 보기 좋게
            EditorGUILayout.BeginHorizontal();

            EditorGUILayout.PropertyField(actionsProp.GetArrayElementAtIndex(i), true);

            if (GUILayout.Button("X", GUILayout.Width(20)))
            {
                // 배열에서 요소를 삭제할 때 올바른 방법을 사용합니다.
                int oldSize = actionsProp.arraySize;
                actionsProp.DeleteArrayElementAtIndex(i);
                if (actionsProp.arraySize == oldSize) { // Clear a managed reference if the element was not removed
                    actionsProp.DeleteArrayElementAtIndex(i);
                }
                break;
            }
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.EndVertical(); // 박스 닫기
        }

        EditorGUILayout.Space();

        if (GUILayout.Button("Add Action"))
        {
            ShowAddActionMenu(actionsProp);
        }

        // 변경점 3: 변경된 사항을 적용합니다.
        serializedObject.ApplyModifiedProperties();
    }

    private void ShowAddActionMenu(SerializedProperty actionsProp)
    {
        var menu = new GenericMenu();

        var actionTypes = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(assembly => assembly.GetTypes())
            .Where(type => typeof(IAction).IsAssignableFrom(type) &&
                           !type.IsInterface &&
                           !type.IsAbstract);

        foreach (var type in actionTypes)
        {
            menu.AddItem(new GUIContent(type.Name), false, () => {
                actionsProp.arraySize++;
                var newElement = actionsProp.GetArrayElementAtIndex(actionsProp.arraySize - 1);
                newElement.managedReferenceValue = Activator.CreateInstance(type);
                serializedObject.ApplyModifiedProperties();
            });
        }

        menu.ShowAsContext();
    }
}