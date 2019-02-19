using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State
{
    public new AbilityStateMachine sm;
    public IdleState(AbilityStateMachine sm): base(sm)
    {
        this.sm = sm;
    }

    public override void Update(InputFrame input, GameObject obj)
    {
        base.Update(input, obj);

        UseAbility(input);
    }

    public void UseAbility(InputFrame input)
    {
        if (sm.ability == null) { return; }
        if (sm.AbilityPress && sm.abilityReady)
            sm.NextState(sm.ability.InstantiateState(sm));
    }
}
