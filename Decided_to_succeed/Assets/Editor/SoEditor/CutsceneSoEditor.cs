// Editor/actionsequenceEditor.cs

using System;
using System.Linq;
using ScriptableObjects;
using ScriptableObjects.Action;
using UnityEditor;
using UnityEngine;

namespace Editor.SoEditor
{
    [CustomEditor(typeof(Cutscene))]
    public class CutsceneSoEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            var actionsProp = serializedObject.FindProperty("actions");

            EditorGUILayout.LabelField("actions", EditorStyles.boldLabel);

            for (int i = 0; i < actionsProp.arraySize; i++)
            {
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.PropertyField(actionsProp.GetArrayElementAtIndex(i), true);

                if (GUILayout.Button("X", GUILayout.Width(20)))
                {
                    actionsProp.DeleteArrayElementAtIndex(i);
                    break;
                }
                EditorGUILayout.EndHorizontal();
            }

            EditorGUILayout.Space();
            if (GUILayout.Button("Add Action"))
            {
                ShowAddActionMenu(actionsProp);
            }

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
}