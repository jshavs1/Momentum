using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityStateMachine : StateMachine
{
    public AbilityProfile ability1;
    public AbilityProfile ability2;

    public float cooldown1, cooldown2;
    public bool ability1Active, ability2Active;

    public override State StartingState()
    {
        return new IdleState(this);
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();

        if (cooldown1 > 0f)
            cooldown1 -= Time.fixedDeltaTime;
        if (cooldown2 > 0f)
            cooldown2 -= Time.fixedDeltaTime;
    }

    public bool ability1Ready
    {
        get
        {
            return cooldown1 <= 0f;
        }
    }

    public bool ability2Ready
    {
        get
        {
            return cooldown2 <= 0f;
        }
    }
}
