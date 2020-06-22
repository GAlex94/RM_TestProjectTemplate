using System;
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
            RecalculateScale();
            unit.recalculateWinner?.Invoke();
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
                unit.recalculateWinner?.Invoke();
            }
        }

        private void RecalculateScale()
        {
            var scale = transform.localScale.x - speedLowScale * Time.deltaTime * GameManager.Instance.SpeedSimulate;
            scale = Mathf.Clamp(scale, 0, Single.MaxValue);
            transform.localScale = new Vector3(scale, scale, 1);
        }
    }
}