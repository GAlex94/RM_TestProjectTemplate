using System;
using UnityEngine;

namespace testProjectTemplate
{
    [CreateAssetMenu(fileName = "UnitsConfig", menuName = "Data/Unit/UnitsConfig")]
    public class UnitsConfig : ScriptableObject
    {
        public UnitInfo[] infoUnits;
    }

    [Serializable]
    public class UnitInfo
    {
#if UNITY_EDITOR
        public string NameEditor;
#endif
        public BasicUnit unitPrefab;
        public UnitTypeEnum unitType;
        public Color teamColor;
    }

    public enum UnitTypeEnum
    {
        Blue,
        Red,
    }
}