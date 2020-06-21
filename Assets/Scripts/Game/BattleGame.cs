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

        public float TimeSimulate { get; set; }

        public void StartGame()
        {
            gameAreaController.Init(GameManager.Instance.CurrentGameSetting.gameAreaWidth,GameManager.Instance.CurrentGameSetting.gameAreaHeight);
            spawnController.Init();
            unitController.Init(unitsConfig, GameManager.Instance.CurrentGameSetting);
            TimeSimulate = GameManager.Instance.TimeSimulate;

            GUIController.Instance.ShowScreen<ScreenGame>();
            var screenTopBar = GUIController.Instance.FoundScreen<ScreenTopBar>();
            screenTopBar.Init();
            GUIController.Instance.ShowScreen(screenTopBar);
        }

        public void Win(UnitTypeEnum teamWin)
        {
            GameManager.Instance.SetStateGame(StateGameEnum.Pause);
            var screenWin = GUIController.Instance.FoundScreen<ScreenPopupWin>();
            screenWin.Init(teamWin.ToString(), TimeSimulate);
            GUIController.Instance.ShowScreen(screenWin);
        }

        public void StartSimulate()
        {
            if (GameManager.Instance.IsLoadGame)
            {
                GameManager.Instance.SetStateGame(StateGameEnum.Pause);
                unitController.LoadData();
            }
            else
            {
                unitController.RecalculateWinner();
                unitController.TeamOne.units.ForEach(u => u.ChangeState(UnitStateEnum.RecalculateDirect));
                unitController.TeamTwo.units.ForEach(u => u.ChangeState(UnitStateEnum.RecalculateDirect));
            }

        }

        private void Update()
        {
            if (GameManager.Instance.CurrentStateGame == StateGameEnum.Game)
            {
                TimeSimulate += Time.deltaTime;
            }
        }
    }

}