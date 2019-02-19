using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbilityProfile : ScriptableObject
{
    public float cooldown;
    public float duration;
    public bool beginCoolDownOnCompletion, disableMovement, disableGun, disableAbilities;


    protected abstract System.Type AbilityStateType { get; }

    public AbilityState InstantiateState(StateMachine sm)
    {
        AbilityState state = (AbilityState)Activator.CreateInstance(AbilityStateType, new object[] { sm });
        state.ability = this;
        state.remainingDuration = duration;
        Initialize(state);
        return state;
    }

    protected abstract void Initialize(AbilityState state);
}
