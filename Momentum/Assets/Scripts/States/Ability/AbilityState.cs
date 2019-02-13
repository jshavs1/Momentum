using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityState : State
{
    public new AbilityStateMachine sm;

    public AbilityProfile ability;
    public int abilitySlot;
    public float remainingDuration;

    public AbilityState(AbilityStateMachine sm) : base(sm)
    {
        this.sm = sm;
    }

    public override void Enter(InputFrame input, GameObject obj)
    {
        base.Enter(input, obj);
        abilitySlot = input.Ability2Press ? 1 : 2;

        SetActive(ability.passive);
        if (!ability.beginCoolDownOnCompletion)
            SetCooldown();
    }

    public override void FixedUpdate(InputFrame input, GameObject obj)
    {
        base.FixedUpdate(input, obj);

        remainingDuration -= Time.fixedDeltaTime;
        if (remainingDuration <= 0f)
            sm.NextState(new IdleState(sm));
    }

    public override void Exit(InputFrame input, GameObject obj)
    {
        base.Exit(input, obj);

        SetActive(false);
        if (ability.beginCoolDownOnCompletion)
            SetCooldown();
    }

    public void SetCooldown()
    {
        if (abilitySlot == 1)
            sm.cooldown1 = ability.cooldown;
        else if (abilitySlot == 2)
            sm.cooldown2 = ability.cooldown;
    }

    public void SetActive(bool active)
    {
        if (abilitySlot == 1)
            sm.ability1Active = active;
        else if (abilitySlot == 2)
            sm.ability2Active = active;
    }

    public bool AbilityHold
    {
        get
        {
            return abilitySlot == 1 ? InputFrame.input.Ability2Hold : InputFrame.input.Ability3Hold;
        }
    }

    public bool AbilityPress
    {
        get
        {
            return abilitySlot == 1 ? InputFrame.input.Ability2Press : InputFrame.input.Ability3Press;
        }
    }
}
