using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace testProjectTemplate
{
    public class UnitController : MonoBehaviour
    {
        private UnitsConfig unitsConfig;
        private TeamUnit teamOne;
        private TeamUnit teamTwo;
        private float winOne;
        private float winTwo;

        private List<IScoreWinnerListener> scoreListeners = new List<IScoreWinnerListener>();
        public TeamUnit TeamOne => teamOne;

        public TeamUnit TeamTwo => teamTwo;

        public void Init(UnitsConfig unitsConfig, GameConfig settingGame, UnitTypeEnum typeTeamOne = UnitTypeEnum.Blue, UnitTypeEnum typeTeamTwo = UnitTypeEnum.Red)
        {
            this.unitsConfig = unitsConfig;
            CreateTeams(settingGame, typeTeamOne, typeTeamTwo);
        }

        private void CreateTeams(GameConfig settingGame, UnitTypeEnum typeTeamOne, UnitTypeEnum typeTeamTwo)
        {
            teamOne = new TeamUnit(typeTeamOne, GetInfoUnit(typeTeamOne).teamColor);
            teamTwo = new TeamUnit(typeTeamTwo, GetInfoUnit(typeTeamTwo).teamColor);
            BattleGame.Instance.SpawnController.SpawnUnits(settingGame.numUnitsToSpawn, settingGame.unitSpawnDelay, GetInfoUnit(typeTeamOne), GetInfoUnit(typeTeamTwo), AddUnitInTeam, BattleGame.Instance.StartSimulate);
            winOne = winTwo = 0.5f;
        }

        private UnitInfo GetInfoUnit(UnitTypeEnum typeUnit)
        {
            return unitsConfig.infoUnits.First(un => un.unitType == typeUnit);
        }

        public void AddUnitInTeam(BasicUnit unit, UnitTypeEnum typeTeam)
        {
            var setting = GameManager.Instance.CurrentGameSetting;
            if (teamOne.teamType == typeTeam)
            {
                unit.Init(typeTeam, UnityEngine.Random.Range(setting.unitSpawnMinRadius, setting.unitSpawnMaxRadius), setting.unitDestroyRadius, UnityEngine.Random.Range(setting.unitSpawnMinSpeed, setting.unitSpawnMaxSpeed), CheckCollision, RecalculateWinner);
                teamOne.units.Add(unit);
            }
            else
            {
                unit.Init(typeTeam,UnityEngine.Random.Range(setting.unitSpawnMinRadius, setting.unitSpawnMaxRadius), setting.unitDestroyRadius, UnityEngine.Random.Range(setting.unitSpawnMinSpeed, setting.unitSpawnMaxSpeed), CheckCollision, RecalculateWinner);
                teamTwo.units.Add(unit);
            }
        }

        public void RecalculateWinner()
        {
            if (GameManager.Instance.CurrentStateGame != StateGameEnum.Game)
            {
                return;
            }

            var sumTeamOne = 0f;
            foreach (var curUnit in teamOne.units)
            {
                sumTeamOne += curUnit.Size;
            }

            var sumTeamTwo = 0f;
            foreach (var curUnit in teamTwo.units)
            {
                sumTeamTwo += curUnit.Size;
            }

            var sumUnits = sumTeamOne + sumTeamTwo;
            winOne = (float)sumTeamOne / sumUnits;
            winTwo = (float)sumTeamTwo / sumUnits;

            if (winOne <=0)
            {
                BattleGame.Instance.Win(teamTwo.teamType);
            }

            if (winTwo <= 0)
            {
                BattleGame.Instance.Win(teamOne.teamType);
            }
            scoreListeners.ForEach(curListener => curListener.OnScoreChange(winOne, winTwo));
        }

        private void CheckCollision(BasicUnit unit, UnitTypeEnum unitType)
        {
            var teamEmptyUnit = teamOne.teamType == unitType ? teamTwo : teamOne;
            var teamFriendlyUnit = teamOne.teamType != unitType ? teamTwo : teamOne;
          
            foreach (var otherUnit in teamFriendlyUnit.units)
            {
                if (otherUnit != unit && Vector3.Distance(otherUnit.transform.position, unit.transform.position) < (otherUnit.transform.localScale.x / 2 + unit.transform.localScale.x / 2))
                {
                    unit.ChangeState(UnitStateEnum.RecalculateDirect);
                    otherUnit.ChangeState(UnitStateEnum.RecalculateDirect);
                }
            }

            foreach (var emptyUnit in teamEmptyUnit.units)
            {
                if (Vector3.Distance(emptyUnit.transform.position, unit.transform.position) < (emptyUnit.transform.localScale.x / 2 + unit.transform.localScale.x / 2))
                {
                    unit.ChangeState(UnitStateEnum.Damage);
                    emptyUnit.ChangeState(UnitStateEnum.Damage);
                }
                else
                {
                    unit.ChangeState(UnitStateEnum.Walk);
                    emptyUnit.ChangeState(UnitStateEnum.Walk);
                }
            }
        }

        public void DeleteUnit(BasicUnit basicUnit)
        {
            var teamUnit = teamOne.teamType == basicUnit.UnitType ? teamOne : teamTwo;
            teamUnit.units.Remove(basicUnit);
            BattleGame.Instance.ObjectsPoolController.DestroyFromPool(basicUnit.gameObject);
            RecalculateWinner();
        }

        public void AddScoreListener(IScoreWinnerListener listener)
        {
            if (!scoreListeners.Contains(listener))
                scoreListeners.Add(listener);
        }

        public void RemoveScoreListener(IScoreWinnerListener listener)
        {
            scoreListeners.Remove(listener);
        }

        public void ClearGame()
        {
            foreach (var curUnit in teamOne.units)
            {
                BattleGame.Instance.ObjectsPoolController.DestroyFromPool(curUnit.gameObject);
            }

            teamOne.units.Clear();

            foreach (var curUnit in teamTwo.units)
            {
                BattleGame.Instance.ObjectsPoolController.DestroyFromPool(curUnit.gameObject);
            }

            teamOne.units.Clear();

        }

        public void LoadData()
        {
            var dataTeamOne = DataManager.Instance.CurrentData.BattleData.teamOne;
            var dataTeamTwo = DataManager.Instance.CurrentData.BattleData.teamTwo;
            for (var i = 0; i < teamOne.units.Count; i++)
            {
                if (i < dataTeamOne.Count)
                {
                    teamOne.units[i].ReInit(dataTeamOne[i].position, dataTeamOne[i].currentSize,
                        dataTeamOne[i].currentSpeed, dataTeamOne[i].targetVector, dataTeamOne[i].typeUnit,
                        dataTeamOne[i].stateUnit);
                    teamOne.units[i].gameObject.SetActive(true);
                }
                else
                {
                    BattleGame.Instance.ObjectsPoolController.DestroyFromPool(teamOne.units[i].gameObject);
                }
            }

            for (var i = 0; i < teamTwo.units.Count; i++)
            {
                if (i < dataTeamTwo.Count)
                {
                    teamTwo.units[i].ReInit(dataTeamTwo[i].position, dataTeamTwo[i].currentSize,
                        dataTeamTwo[i].currentSpeed, dataTeamTwo[i].targetVector, dataTeamTwo[i].typeUnit,
                        dataTeamTwo[i].stateUnit);
                    teamTwo.units[i].gameObject.SetActive(true);
                }
                else
                {
                    BattleGame.Instance.ObjectsPoolController.DestroyFromPool(teamTwo.units[i].gameObject);
                }
            }
            teamOne.units.RemoveAll(u => !u.gameObject.activeSelf);
            teamTwo.units.RemoveAll(u => !u.gameObject.activeSelf);

            GameManager.Instance.SetStateGame(StateGameEnum.Game);
            RecalculateWinner();
        }
    }


    [Serializable]
    public class TeamUnit
    {
        public UnitTypeEnum teamType;
        public Color teamColor;
        public List<BasicUnit> units;

        public TeamUnit(UnitTypeEnum teamType, Color teamColor)
        {
            this.teamType = teamType;
            this.teamColor = teamColor;
            units = new List<BasicUnit>();
        }
    }
}