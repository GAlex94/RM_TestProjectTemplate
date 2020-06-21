using System;

namespace testProjectTemplate
{
    [Serializable]
    public class MainSettingGame
    {
        public GameConfig GameConfig;
    }


    [Serializable]
    public class GameConfig
    {
        public int gameAreaWidth;
        public int gameAreaHeight;
        public int numUnitsToSpawn;
        public float unitSpawnDelay;
        public float unitSpawnMinRadius;
        public float unitSpawnMaxRadius;
        public float unitSpawnMinSpeed;
        public float unitSpawnMaxSpeed;
        public float unitDestroyRadius;

        public GameConfig()
        {
            gameAreaWidth = 0;
            gameAreaHeight = 0;
            numUnitsToSpawn = 0;
            unitSpawnDelay = 0;
            unitSpawnMinRadius = 0;
            unitSpawnMaxRadius = 0;
            unitSpawnMinSpeed = 0;
            unitSpawnMaxSpeed = 0;
            unitDestroyRadius = 0;
        }

        public GameConfig(int gameAreaWidth, int gameAreaHeight, int numUnitsToSpawn, float unitSpawnDelay, float unitSpawnMinRadius, float unitSpawnMaxRadius, float unitSpawnMinSpeed, float unitSpawnMaxSpeed, float unitDestroyRadius)
        {
            this.gameAreaWidth = gameAreaWidth;
            this.gameAreaHeight = gameAreaHeight;
            this.numUnitsToSpawn = numUnitsToSpawn;
            this.unitSpawnDelay = unitSpawnDelay;
            this.unitSpawnMinRadius = unitSpawnMinRadius;
            this.unitSpawnMaxRadius = unitSpawnMaxRadius;
            this.unitSpawnMinSpeed = unitSpawnMinSpeed;
            this.unitSpawnMaxSpeed = unitSpawnMaxSpeed;
            this.unitDestroyRadius = unitDestroyRadius;
        }
    }
}