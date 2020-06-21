﻿using TMPro;
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
        }

        private void LoadGame()
        {
            GameManager.Instance.SetStateGame(StateGameEnum.Pause);
            throw new System.NotImplementedException();
        }

        private void SaveGame()
        {
            DataManager.Instance.Save(true);
        }

        private void NewGame()
        {
            GameManager.Instance.SetStateGame(StateGameEnum.Pause);
        }
    }
}