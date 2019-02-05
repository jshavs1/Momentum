using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunState : State
{
    public new GunSM sm;
    public GunState(GunSM sm): base(sm)
    {
        this.sm = sm;
    }

    public override void Update(InputFrame input, GameObject obj)
    {
        base.Update(input, obj);

        if (input.PrimaryHold)
            sm.Shoot();
    }

    public override void FixedUpdate(InputFrame input, GameObject obj)
    {
        base.FixedUpdate(input, obj);

        SetGunParams();

    }

    public virtual void SetGunParams()
    {
        if (isGrounded)
        {
            sm.maxSpread = sm.gunProfile.maxSpread;
            sm.minSpread = sm.gunProfile.minSpread;
            sm.spreadRate = sm.gunProfile.spreadRate;
            sm.spreadRecoveryRate = sm.gunProfile.spreadRecoveryRate;
        }
        else
        {
            sm.maxSpread = sm.gunProfile.airMaxSpread;
            sm.minSpread = sm.gunProfile.airMinSpread;
            sm.spreadRate = sm.gunProfile.airSpreadRate;
            sm.spreadRecoveryRate = sm.gunProfile.airSpreadRecoveryRate;
        }

    }
}
