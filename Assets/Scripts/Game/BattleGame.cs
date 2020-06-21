using System.Collections;
using UnityEngine;

namespace testProjectTemplate
{
    public class BattleGame : Singleton<BattleGame>, IGame
    {
        [Header("Конфиги")]
       [SerializeField] private UnitsConfig unitsConfig;

        [Header("Контроллеры")]
        [SerializeField] private GameAreaController gameAreaController;
        [SerializeField] private SpawnController spawnController;
        [SerializeField] private ObjectsPoolController objectsPoolController;
        [SerializeField] private UnitController unitController;

        public GameAreaController GameAreaController => gameAreaController;
        public ObjectsPoolController ObjectsPoolController => objectsPoolController;
        public SpawnController SpawnController => spawnController;
        public UnitController UnitController => unitController;

        public void StartGame()
        {
            gameAreaController.Init(GameManager.Instance.MainGameSetting.gameAreaWidth, GameManager.Instance.MainGameSetting.gameAreaHeight);
            unitController.Init(unitsConfig, GameManager.Instance.MainGameSetting);
            spawnController.Init();

        }

        public void Win(UnitTypeEnum teamOneTeamType)
        {
            GameManager.Instance.SetStateGame(StateGameEnum.Pause);
        }
    }

}