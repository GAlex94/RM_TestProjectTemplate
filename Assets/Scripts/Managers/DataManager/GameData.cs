using System;
using System.Collections.Generic;
using UnityEngine;

namespace testProjectTemplate
{
    [Serializable]
    public class GameData
    {
        public string Version;
        public BattleData BattleData;

        public GameData()
        {
            Version = "1.0";
        }
    }

    [Serializable]
    public class BattleData
    {
        public GameConfig settingGame;
        public List<SaveUnitInfo> teamOne;
        public List<SaveUnitInfo> teamTwo;
    }


    [Serializable]
    public class SaveUnitInfo
    {
        public Vector3 position;
        public UnitTypeEnum typeUnit;
        public UnitStateEnum stateUnit;
        public Vector3 targetVector;
        public float currentSpeed;
        public float currentSize;

        public SaveUnitInfo(Vector3 position, UnitTypeEnum typeUnit, UnitStateEnum stateUnit, Vector3 targetVector, float currentSpeed, float currentSize)
        {
            this.position = position;
            this.typeUnit = typeUnit;
            this.stateUnit = stateUnit;
            this.targetVector = targetVector;
            this.currentSpeed = currentSpeed;
            this.currentSize = currentSize;
        }
    }
}