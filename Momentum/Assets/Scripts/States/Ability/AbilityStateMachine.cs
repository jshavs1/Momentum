using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class AbilityStateMachine : StateMachine
{
    public AbilityProfile ability;

    public AbilitySlot abilitySlot;
    public float remainingCooldown;
    public bool abilityActive;

    public override State StartingState()
    {
        return new IdleState(this);
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();

        if (remainingCooldown > 0f)
            remainingCooldown = Mathf.Max(remainingCooldown - Time.fixedDeltaTime, 0f);
    }

    public bool abilityReady
    {
        get
        {
            return remainingCooldown <= 0f;
        }
    }

    public bool AbilityHold
    {
        get
        {
            return abilitySlot == AbilitySlot.Ability1 ? currentInput.Ability2Hold : currentInput.Ability3Hold;
        }
    }

    public bool AbilityPress
    {
        get
        {
            return abilitySlot == AbilitySlot.Ability1 ? currentInput.Ability2Press : currentInput.Ability3Press;
        }
    }
}

public enum AbilitySlot: byte
{
    Ability1 = 0,
    Ability2
}
