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

        if (input.DidScrollUp && sm.gunIndex == 0)
        {
            sm.NextState(new SwitchState(sm, sm.secondaryGun));
            sm.gunIndex = 1;
        }
        else if (input.DidScrollDown && sm.gunIndex == 1)
        {
            sm.NextState(new SwitchState(sm, sm.primaryGun));
            sm.gunIndex = 0;
        }
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
            sm.maxSpread = sm.currentGun.maxSpread;
            sm.minSpread = sm.currentGun.minSpread;
            sm.spreadRate = sm.currentGun.spreadRate;
            sm.spreadRecoveryRate = sm.currentGun.spreadRecoveryRate;
        }
        else
        {
            sm.maxSpread = sm.currentGun.airMaxSpread;
            sm.minSpread = sm.currentGun.airMinSpread;
            sm.spreadRate = sm.currentGun.airSpreadRate;
            sm.spreadRecoveryRate = sm.currentGun.airSpreadRecoveryRate;
        }

    }
}
