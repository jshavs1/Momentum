using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityState : State
{
    public new AbilityStateMachine sm;

    public AbilityProfile ability;
    public float remainingDuration;

    public AbilityState(AbilityStateMachine sm) : base(sm)
    {
        this.sm = sm;
    }

    public override void Enter(InputFrame input, GameObject obj)
    {
        base.Enter(input, obj);

        SetStateMachinesIsDisabled(obj, true);

        if (!ability.beginCoolDownOnCompletion)
            StartCooldown();
    }

    public override void FixedUpdate(InputFrame input, GameObject obj)
    {
        base.FixedUpdate(input, obj);

        remainingDuration -= Time.fixedDeltaTime;
        if (remainingDuration <= 0f)
        {
            sm.NextState(new IdleState(sm));
            return;
        }
    }

    public override void Exit(InputFrame input, GameObject obj)
    {
        base.Exit(input, obj);

        SetStateMachinesIsDisabled(obj, false);

        if (ability.beginCoolDownOnCompletion)
            StartCooldown();
    }

    public void SetStateMachinesIsDisabled(GameObject obj, bool isDisabled)
    {
        if (ability.disableGun)
            obj.GetComponent<ActionStateMachine>().isDisabled = isDisabled;
        if (ability.disableMovement)
            obj.GetComponent<MovementSM>().isDisabled = isDisabled;
        if (ability.disableAbilities)
        {
            foreach (AbilityStateMachine abs in obj.GetComponents<AbilityStateMachine>())
            {
                if (abs != sm)
                    abs.isDisabled = isDisabled;
            }
        }
    }

    public void StartCooldown()
    {
        sm.remainingCooldown = ability.cooldown;
    }
}
