using UnityEngine;

namespace testProjectTemplate
{
    public class MoveState : UnitState
    {
        private Bounds boundMove;
        private bool isStopped;
        public MoveState()
        {
            stateType = UnitStateEnum.Walk;
        }

        protected override void Awake()
        {
            base.Awake();
            isStopped = true;
            boundMove = new Bounds(Vector3.zero, BattleGame.Instance.GameAreaController.AreaRenderer.bounds.size);
        }


        protected override void Update()
        {
            if (GameManager.Instance.CurrentStateGame != StateGameEnum.Game)
            {
                return;
            }

            base.Update();
            
            if (!isStopped)
            {
                Move();
                unit.currentPositions?.Invoke(unit, unit.UnitType);
            }
        }

        protected override void OnStart()
        {
            base.OnStart();
            isStopped = false;
        }

        protected override void OnFinish()
        {
            base.OnFinish();
            isStopped = true;
        }

        public virtual void Move()
        {
            if (IsMoveInside(transform.position + unit.Speed * unit.Target * Time.deltaTime))
            {
                isStopped = true;
                setNewState(UnitStateEnum.RecalculateDirect, false);
            }
            else
            {
                transform.Translate(unit.Speed * unit.Target * Time.deltaTime * GameManager.Instance.SpeedSimulate);
            }
        }

        private bool IsMoveInside(Vector3 newPos)
        {
            var min = boundMove.min;
            var max = boundMove.max;
            var sizeUnit = transform.localScale.x;
            min.x += sizeUnit / 2;
            min.y += sizeUnit / 2;
            max.x -= sizeUnit / 2;
            max.y -= sizeUnit / 2;

            if ((newPos.x >= min.x && newPos.x <= max.x) && (newPos.y >= min.y && newPos.y <= max.y))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}