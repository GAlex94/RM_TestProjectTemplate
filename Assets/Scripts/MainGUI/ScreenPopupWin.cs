using System;
using UnityEngine;
using UnityEngine.UI;

namespace testProjectTemplate
{
    public class ScreenPopupWin : GUIScreen
    {
        [Header("ScreenPopupWin")]
        [SerializeField] private Text messageText;
        [SerializeField] private Button restartButton;

        private void Awake()
        {
            restartButton.onClick.RemoveAllListeners();
            restartButton.onClick.AddListener(Restart);
        }

        public void Init(string teamWin, float timeSimulation)
        {
            messageText.text = String.Format("Победитель: {0}\nВремя симуляции: {1} мс", teamWin, timeSimulation);
        }

        private void Restart()
        {
            GameManager.Instance.SetStateGame(StateGameEnum.Pause);
            BattleGame.Instance.UnitController.ClearGame();
            GUIController.Instance.HideAll();
            GameManager.Instance.TimeSimulate = 0;
            GameManager.Instance.IsLoadGame = false;
            GameManager.Instance.SetCurrentSetting(GameManager.Instance.MainSetting);
            GameManager.Instance.StartGame(StateGameEnum.Game);
        }

      
    }
}