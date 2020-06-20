using System.IO;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace Assets.Editor
{
    public class SceneItem : UnityEditor.Editor
    {
        [MenuItem("Open Scene/Game")]
        public static void OpenGame()
        {
            OpenScene("game");
        }

        static void OpenScene(string name)
        {
            if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
            {
                EditorSceneManager.OpenScene("Assets/Scenes/" + name + ".unity");
            }
        }
    }
}
