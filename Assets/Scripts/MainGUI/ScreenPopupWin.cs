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
            throw new System.NotImplementedException();
        }

      
    }
}