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

        if (UseAbility1(input))
            sm.NextState(sm.ability1.InstantiateState(sm));
        if (UseAbility2(input))
            sm.NextState(sm.ability2.InstantiateState(sm));
    }

    public bool UseAbility1(InputFrame input)
    {
        if (sm.ability1 == null) { return false; }
        if (input.Ability2Press && sm.ability1Ready)
        {
            if (sm.ability2.passive || !sm.ability2Active)
                return true;
        }
        return false;
    }

    public bool UseAbility2(InputFrame input)
    {
        if (sm.ability2 == null) { return false; }
        if (input.Ability3Press && sm.ability2Ready)
        {
            if (sm.ability1.passive || !sm.ability1Active)
                return true;
        }
        return false;
    }
}
