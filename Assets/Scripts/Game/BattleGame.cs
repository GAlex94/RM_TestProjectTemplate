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

        private float timeSimulate;

        public void StartGame()
        {
            gameAreaController.Init(GameManager.Instance.MainGameSetting.gameAreaWidth, GameManager.Instance.MainGameSetting.gameAreaHeight);
            unitController.Init(unitsConfig, GameManager.Instance.MainGameSetting);
            spawnController.Init();


            GUIController.Instance.ShowScreen<ScreenGame>();
            var screenTopBar = GUIController.Instance.FoundScreen<ScreenTopBar>();
            screenTopBar.Init();
            GUIController.Instance.ShowScreen(screenTopBar);
        }

        public void Win(UnitTypeEnum teamWin)
        {
            GameManager.Instance.SetStateGame(StateGameEnum.Pause);
            var screenWin = GUIController.Instance.FoundScreen<ScreenPopupWin>();
            screenWin.Init(teamWin.ToString(), timeSimulate);
            GUIController.Instance.ShowScreen(screenWin);
        }

        public void StartSimulate()
        {
            unitController.RecalculateWinner();
            unitController.TeamOne.units.ForEach(u=>u.ChangeState(UnitStateEnum.RecalculateDirect));
            unitController.TeamTwo.units.ForEach(u=>u.ChangeState(UnitStateEnum.RecalculateDirect));
            
        }

        private void Update()
        {
            if (GameManager.Instance.CurrentStateGame == StateGameEnum.Game)
            {
                timeSimulate += Time.deltaTime;
            }
        }
    }

}