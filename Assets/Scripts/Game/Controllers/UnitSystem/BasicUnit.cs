using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace testProjectTemplate
{
    public class BasicUnit : Unit
    {
        [SerializeField] private List<UnitState> statesList;
        private Dictionary<UnitStateEnum, UnitState> states;

        internal Action recalculateWinner;
        internal Action<BasicUnit, UnitTypeEnum> currentPositions;
        public UnitTypeEnum UnitType { get; set; }
        public UnitState UnitState => currentState;
        public Vector3 Target { get; set; }
        public float Speed { get; private set; }
        public float SizeDeath { get; private set; }
        public bool IsDeath { get; private set; }
        public float Size => transform.localScale.x;

        public void Init(UnitTypeEnum unitType, float size, float sizeDeath, float speed,
            Action<BasicUnit, UnitTypeEnum> currentPositions, Action recalculateWinner)
        {
            UnitType = unitType;
            SizeDeath = sizeDeath;

            transform.localScale = new Vector3(size, size, 1);
            this.recalculateWinner = recalculateWinner;
            this.currentPositions = currentPositions;
            Speed = speed;

            foreach (var foundState in statesList)
            {
                foundState.Init(this, ChangeState);
            }

            states = statesList.ToDictionary(newState => newState.StateType);
            ChangeState(UnitStateEnum.Idle);
        }

        public void ChangeState(UnitStateEnum newState, bool force = false)
        {
            if (IsDeath && !force || (currentState != null && newState == currentState.StateType))
            {
                return;
            }

            var newSate = FoundState(newState);

            if (newSate == null) return;

            if (currentState != null)
            {
                currentState.DeactivateState();
            }

            newSate.ActivateState();
            currentState = newSate;
        }

        private UnitState FoundState(UnitStateEnum stateType)
        {
            UnitState foundState;
            if (states.TryGetValue(stateType, out foundState))
            {
                return foundState;
            }

            return null;
        }

        private void Update()
        {
            if (SizeDeath >= transform.localScale.x)
            {
                IsDeath = true;
                ChangeState(UnitStateEnum.Death, true);
            }
        }
        
        public void ReInit(Vector3 position, float currentSize, float currentSpeed, Vector3 targetVector, UnitTypeEnum typeUnit, UnitStateEnum stateUnit)
        {
            transform.position = position;
            transform.localScale = new Vector3(currentSize, currentSize, 1);
            Target = targetVector;
            UnitType = typeUnit;
            Speed = currentSpeed;
            ChangeState(stateUnit, true);
        }
    }
}