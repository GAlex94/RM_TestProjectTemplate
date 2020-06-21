using UnityEngine;
using UnityEngine.AI;

namespace testProjectTemplate
{
    public abstract class Unit: MonoBehaviour
    {
        protected UnitState currentState;
    }
    public enum UnitStateEnum
    {
        Idle,
        Walk,
        RecalculateDirect,
        Damage,
        Death,
    }
}
