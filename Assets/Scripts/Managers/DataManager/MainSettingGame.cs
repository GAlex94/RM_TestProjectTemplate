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
    }
}