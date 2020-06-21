using UnityEngine;

namespace testProjectTemplate
{
    public class DeathState : UnitState
    {
        private DeathState()
        {
            stateType = UnitStateEnum.Death;
        }

        protected override void OnStart()
        {
            base.OnStart();
            Death();
        }

        private void Death()
        {
            BattleGame.Instance.UnitController.DeleteUnit(unit);
            unit.recalculateWinner?.Invoke();
            setNewState(UnitStateEnum.Idle, false);
        }
    }
}