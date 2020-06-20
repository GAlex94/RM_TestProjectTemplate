using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

namespace testProjectTemplate
{
    [CreateAssetMenu(fileName = "DefaultProfile", menuName = "Data/BasicConfig/DefaultProfile")]
    public class DefaultProfile : ScriptableObject
    {
        public GameData profileData;
    }

}