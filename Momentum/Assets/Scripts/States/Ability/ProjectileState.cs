using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ProjectileState : AbilityState
{
    public new ProjectileAbilityProfile ability;
    int remainingAmmo;
    float fireRate;
    float timeUntilNextProjectile;

    public ProjectileState(AbilityStateMachine sm): base(sm) { }

    public override void Enter(InputFrame input, GameObject obj)
    {
        base.Enter(input, obj);

        remainingAmmo = ability.ammo;

        if (!ability.firstShotDelay)
        {
            obj.GetComponent<ProjectileTrigger>()?.ShootProjectile(sm.abilitySlot);
            remainingAmmo--;
        }

        fireRate = ability.duration / (float)remainingAmmo;
        timeUntilNextProjectile = fireRate;
    }

    public override void FixedUpdate(InputFrame input, GameObject obj)
    {
        timeUntilNextProjectile = timeUntilNextProjectile - Time.fixedDeltaTime;
        if (remainingAmmo <= 0)
            sm.NextState(new IdleState(sm));
        if (timeUntilNextProjectile <= 0f)
        {
            obj.GetComponent<ProjectileTrigger>()?.ShootProjectile(sm.abilitySlot);

            timeUntilNextProjectile = fireRate;
            remainingAmmo--;
        }
        base.FixedUpdate(input, obj);
    }
}
