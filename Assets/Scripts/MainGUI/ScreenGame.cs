using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace testProjectTemplate
{
    public class ScreenGame : GUIScreen
    {
        [Header("ScreenGame")]
        [SerializeField] private Button newGameButton;
        [SerializeField] private Button saveButton;
        [SerializeField] private Button loadButton;
        [SerializeField] private Slider speedSlider;

        private void Awake()
        {
            newGameButton.onClick.RemoveAllListeners();
            newGameButton.onClick.AddListener(NewGame);

            saveButton.onClick.RemoveAllListeners();
            saveButton.onClick.AddListener(SaveGame);

            loadButton.onClick.RemoveAllListeners();
            loadButton.onClick.AddListener(LoadGame);

            speedSlider.onValueChanged.RemoveAllListeners();
            speedSlider.onValueChanged.AddListener(SetSpeed);
        }

        protected override void OnShow()
        {
            base.OnShow();
            speedSlider.value = 1;
            GameManager.Instance.SpeedSimulate = 1;
            loadButton.interactable = DataManager.Instance.IsCanLoadData();
        }

        private void SetSpeed(float value)
        {
            GameManager.Instance.SpeedSimulate = value;
        }

        private void LoadGame()
        {
            GameManager.Instance.SetStateGame(StateGameEnum.Pause);
            BattleGame.Instance.UnitController.ClearGame();
            DataManager.Instance.LoadData();
            GameManager.Instance.StartGame(StateGameEnum.Game);
        }

        private void SaveGame()
        {
            DataManager.Instance.Save(false);
            loadButton.interactable = DataManager.Instance.IsCanLoadData();
        }

        private void NewGame()
        {
            GameManager.Instance.SetStateGame(StateGameEnum.Pause);
            BattleGame.Instance.UnitController.ClearGame();
            GameManager.Instance.TimeSimulate = 0;
            GameManager.Instance.SetCurrentSetting(GameManager.Instance.MainSetting);
            GameManager.Instance.StartGame(StateGameEnum.Game);
        }
    }
}