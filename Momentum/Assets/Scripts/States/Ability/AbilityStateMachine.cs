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
            switch(abilitySlot)
            {
                case AbilitySlot.Primary:
                    return currentInput.PrimaryHold;
                case AbilitySlot.Secondary:
                    return currentInput.SecondaryHold;
                case AbilitySlot.Ability3:
                    return currentInput.Ability3Hold;
                case AbilitySlot.Ability4:
                    return currentInput.Ability4Hold;
                case AbilitySlot.Ability5:
                    return currentInput.Ability5Hold;
                default:
                    return false;
            }
        }
    }

    public bool AbilityPress
    {
        get
        {
            switch (abilitySlot)
            {
                case AbilitySlot.Primary:
                    return currentInput.PrimaryPress;
                case AbilitySlot.Secondary:
                    return currentInput.SecondaryPress;
                case AbilitySlot.Ability3:
                    return currentInput.Ability3Press;
                case AbilitySlot.Ability4:
                    return currentInput.Ability4Press;
                case AbilitySlot.Ability5:
                    return currentInput.Ability5Press;
                default:
                    return false;
            }
        }
    }
}
