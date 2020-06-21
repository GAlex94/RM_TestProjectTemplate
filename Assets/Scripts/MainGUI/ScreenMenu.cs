using UnityEngine;
using UnityEngine.UI;

namespace testProjectTemplate
{
    public class ScreenMenu : GUIScreen
    {
        [Header("ScreenMenu")]
        [SerializeField] private Button startButton;

        private void Awake()
        {
            startButton.onClick.RemoveAllListeners();
            startButton.onClick.AddListener(StartGame);
        }

        private void StartGame()
        {
            GameManager.Instance.TimeSimulate = 0; 
            GameManager.Instance.IsLoadGame = false;
            GameManager.Instance.StartGame(StateGameEnum.Game);
            GUIController.Instance.HideScreen(this);
        }
    }
}