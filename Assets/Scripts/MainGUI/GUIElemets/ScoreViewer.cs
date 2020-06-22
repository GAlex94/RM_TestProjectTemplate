using System;
using UnityEngine;
using UnityEngine.UI;

namespace testProjectTemplate
{
    [Serializable]
    public class ScoreViewer : IScoreWinnerListener
    {
        [SerializeField] private Image teamOneImage;
        [SerializeField] private Image teamTwoImage;
       
        public void Init(Color colorTeamOne, Color colorTeamTwo)
        {
            teamOneImage.color = colorTeamOne;
            teamTwoImage.color = colorTeamTwo;
            OnScoreChange(0.5f, 0.5f);
            BattleGame.Instance.UnitController.AddScoreListener(this);
        }

        public void OnDestroy()
        {
            if (BattleGame.IsAwake)
                BattleGame.Instance.UnitController.RemoveScoreListener(this);
        }
        
        public void OnScoreChange(float teamOne, float teamTwo)
        {
            teamOneImage.fillAmount = teamOne;
            teamTwoImage.fillAmount = teamTwo;
        }
    }

    public interface IScoreWinnerListener
    {
        void OnScoreChange(float teamOne, float teamTwo);
    }
}