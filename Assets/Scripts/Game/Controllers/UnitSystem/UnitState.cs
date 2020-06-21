using System;
using UnityEngine;

namespace testProjectTemplate
{
    public abstract class UnitState : MonoBehaviour
    {
        protected UnitStateEnum stateType;
        protected BasicUnit unit;
 
        protected Action<UnitStateEnum, bool> setNewState;
        public UnitStateEnum StateType => stateType;

        public void Init(BasicUnit unit, Action<UnitStateEnum, bool> setNewState)
        {
            this.unit = unit;
            this.setNewState = setNewState;
        }

        public void ActivateState()
        {
            this.enabled = true;
            OnStart();
        }

        public void DeactivateState()
        {
            OnFinish();
            this.enabled = false;
        }

        protected virtual void Awake()
        {

        }

        protected virtual void OnStart()
        {

        }

        protected virtual void OnFinish()
        {

        }

        protected virtual void Update()
        {
        }

     
    }
}