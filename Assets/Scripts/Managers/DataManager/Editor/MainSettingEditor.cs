using System.IO;
using UnityEditor;
using UnityEngine;

namespace testProjectTemplate
{
    [CustomEditor(typeof(MainSettingTest))]
    public class MainSettingEditor : Editor
    {
        MainSettingTest prop;
        Color orgColor;
        private bool isDisplayDefaultInspector;
        private string nameFileSetting = "GameConfig";
        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            prop = (MainSettingTest)target;
            orgColor = GUI.color;
            
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Базовые настройки (для редактирования в Editor)", EditorStyles.boldLabel);
            EditorGUILayout.LabelField("Настройки будут загружены при старте игры из json", EditorStyles.helpBox);
            EditorGUILayout.BeginHorizontal(EditorStyles.helpBox);
            EditorGUILayout.LabelField("Включить отображение стадартного инспектора", EditorStyles.label, GUILayout.Width(300f));
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
            EditorGUILayout.LabelField("GameConfig", EditorStyles.boldLabel);
            prop.setting.GameConfig.gameAreaWidth = EditorGUILayout.IntField("Ширина игрового поля", prop.setting.GameConfig.gameAreaWidth);
            prop.setting.GameConfig.gameAreaHeight = EditorGUILayout.IntField("Высота игрового поля", prop.setting.GameConfig.gameAreaHeight);
            prop.setting.GameConfig.numUnitsToSpawn = EditorGUILayout.IntField("Количество юнитов одного цвета ", prop.setting.GameConfig.numUnitsToSpawn);
            prop.setting.GameConfig.unitSpawnDelay = EditorGUILayout.FloatField("Задержка между спаунами юнитов", prop.setting.GameConfig.unitSpawnDelay);
            prop.setting.GameConfig.unitSpawnMinRadius = EditorGUILayout.FloatField("Минимальный радиус юнита", prop.setting.GameConfig.unitSpawnMinRadius);
            prop.setting.GameConfig.unitSpawnMaxRadius = EditorGUILayout.FloatField("Максимальный радиус юнита", prop.setting.GameConfig.unitSpawnMaxRadius);
            prop.setting.GameConfig.unitSpawnMinSpeed = EditorGUILayout.FloatField("Минимальная скорость юнита", prop.setting.GameConfig.unitSpawnMinSpeed);
            prop.setting.GameConfig.unitSpawnMaxSpeed = EditorGUILayout.FloatField("Максимальный скорость юнита", prop.setting.GameConfig.unitSpawnMaxSpeed);
            prop.setting.GameConfig.unitDestroyRadius = EditorGUILayout.FloatField("Радиус для уничтожения юнита", prop.setting.GameConfig.unitSpawnMaxSpeed);
       
            
            EditorGUILayout.Space();
            nameFileSetting = EditorGUILayout.TextField("Имя файла с настройками", nameFileSetting);
           


            if (GUILayout.Button("Загрузить данные из json"))
            {
                string filePath = "";
                filePath = Application.dataPath + "/StreamingAssets/" + nameFileSetting + ".json";
                if (File.Exists(filePath))
                {
                    prop.setting = JsonUtility.FromJson<MainSettingGame>(File.ReadAllText(filePath));
                }
                else
                {
                    Debug.LogErrorFormat("Файл: {0} не найден", filePath);
                }
            }

            if (GUILayout.Button("Изменить данные в json"))
            {
                string filePath = "";
                filePath = Application.dataPath + "/StreamingAssets/" + nameFileSetting + ".json";
                File.WriteAllText(filePath, JsonUtility.ToJson(prop.setting, true));
            }


            serializedObject.ApplyModifiedProperties();

            if (GUI.changed)
                EditorUtility.SetDirty(prop);


        }
    }
}