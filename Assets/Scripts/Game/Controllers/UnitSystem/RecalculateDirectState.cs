namespace testProjectTemplate
{
    public class RecalculateDirectState : UnitState
    {
        private RecalculateDirectState()
        {
            stateType = UnitStateEnum.RecalculateDirect;
        }
        protected override void OnStart()
        {
            base.OnStart();
            CalculateTarget();
            setNewState(UnitStateEnum.Walk, false);
        }

        public virtual void CalculateTarget()
        {
            unit.Target = UnityEngine.Random.insideUnitCircle.normalized;
        }
    }
}