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
            transform.localScale = new Vector3(0, 0, 1);
            BattleGame.Instance.UnitController.DeleteUnit(unit);
            setNewState(UnitStateEnum.Idle, false);
        }
    }
}