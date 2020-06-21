using UnityEngine;

namespace testProjectTemplate
{
    public class ScreenTopBar : GUIScreen
    {
        [Header("ScreenGame")] [SerializeField]
        private ScoreViewer scoreViewer;

        public void Init()
        {
            scoreViewer.Init(BattleGame.Instance.UnitController.TeamOne.teamColor, BattleGame.Instance.UnitController.TeamTwo.teamColor);
        }

        private void OnDestroy()
        {
            scoreViewer.OnDestroy();
        }
    }
}