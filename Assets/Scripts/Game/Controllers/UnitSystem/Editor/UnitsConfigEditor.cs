using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace testProjectTemplate
{
    [CustomEditor(typeof(UnitsConfig))]
    public class UnitsConfigEditor : Editor
    {
        UnitsConfig prop;
        Color orgColor;
        private bool isDisplayDefaultInspector;

        List<UnitInfo> units = new List<UnitInfo>();

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            prop = (UnitsConfig) target;
            orgColor = GUI.color;

            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Настройки юнитов", EditorStyles.boldLabel);
            EditorGUILayout.BeginHorizontal(EditorStyles.helpBox);
            EditorGUILayout.LabelField("Включить отображение стадартного инспектора", EditorStyles.label,
                GUILayout.Width(300f));
            isDisplayDefaultInspector = EditorGUILayout.Toggle(isDisplayDefaultInspector, GUILayout.Width(150f));
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.Space();

            if (isDisplayDefaultInspector)
            {
                DrawDefaultInspector();
                EditorGUILayout.Space();
            }

            EditorGUIUtility.labelWidth = 250;
            GUI.color = orgColor;
            units = new List<UnitInfo>();

            if (prop.infoUnits == null)
            {
                prop.infoUnits = new UnitInfo[1];
            }

            if (prop.infoUnits.Length < 1)
            {
                units.Clear();
                units.AddRange(prop.infoUnits);
                UnitInfo newUnitInfo = new UnitInfo();
                units.Add(newUnitInfo);
                prop.infoUnits = units.ToArray();

                serializedObject.ApplyModifiedProperties();

                if (GUI.changed)
                    EditorUtility.SetDirty(prop);
            }

            GUILayout.Label("Юниты: ", EditorStyles.boldLabel);
            for (int i = 0; i < prop.infoUnits.Length; i++)
            {
                EditorGUILayout.BeginVertical(GUI.skin.box);
                EditorGUILayout.Space();
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField(prop.infoUnits[i].NameEditor, EditorStyles.boldLabel);
                if (GUILayout.Button("[Удалить]", GUILayout.Width(80f)))
                {
                    units.Clear();
                    units.AddRange(prop.infoUnits);
                    units.RemoveAt(i);
                    prop.infoUnits = units.ToArray();

                    serializedObject.ApplyModifiedProperties();

                    if (GUI.changed)
                        EditorUtility.SetDirty(prop);
                }

                EditorGUILayout.EndHorizontal();
                EditorGUILayout.Space();

                if (i >= prop.infoUnits.Length)
                    return;

                prop.infoUnits[i].NameEditor =
                    EditorGUILayout.TextField("Название в редакторе", prop.infoUnits[i].NameEditor);
                prop.infoUnits[i].unitType =
                    (UnitTypeEnum) EditorGUILayout.EnumPopup("Тип юнита", prop.infoUnits[i].unitType);
                prop.infoUnits[i].unitPrefab = (BasicUnit) EditorGUILayout.ObjectField("Префаб врага",
                    prop.infoUnits[i].unitPrefab, typeof(BasicUnit), false);
                prop.infoUnits[i].teamColor = EditorGUILayout.ColorField("Цвет юнита", prop.infoUnits[i].teamColor);
                EditorGUILayout.EndVertical();

            }


            GUI.color = Color.green;
            GUI.color = orgColor;

            EditorGUILayout.Space();
            if (GUILayout.Button("Добавить нового юнита"))
            {
                units.Clear();
                units.AddRange(prop.infoUnits);
                UnitInfo newUnitInfo = new UnitInfo();
                units.Add(newUnitInfo);
                prop.infoUnits = units.ToArray();

                serializedObject.ApplyModifiedProperties();

                if (GUI.changed)
                    EditorUtility.SetDirty(prop);
            }


            serializedObject.ApplyModifiedProperties();

            if (GUI.changed)
                EditorUtility.SetDirty(prop);
        }
    }
}