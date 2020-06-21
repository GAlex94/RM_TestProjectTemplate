using UnityEngine;

namespace testProjectTemplate
{
    public class DamageState : UnitState
    {
        [SerializeField] private float speedLowScale = 0.1f;
        private bool isDamage = false;
        private DamageState()
        {
            stateType = UnitStateEnum.Damage;
        }

        protected override void OnStart()
        {
            base.OnStart();
            isDamage = true;
        }

        protected override void OnFinish()
        {
            base.OnFinish();
            isDamage = false;
        }

        protected override void Update()
        {
            if (GameManager.Instance.CurrentStateGame != StateGameEnum.Game)
            {
                return;
            }

            base.Update();

            unit.currentPositions(unit, unit.UnitType);
            if (isDamage)
            {
                RecalculateScale();
            }
        }

        private void RecalculateScale()
        {
            var scale = transform.localScale.x - speedLowScale * Time.deltaTime * GameManager.Instance.SpeedSimulate;
            transform.localScale = new Vector3(scale, scale, 1);
        }
    }
}