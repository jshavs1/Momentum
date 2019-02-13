using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunState : ActionState
{
    public GunState(ActionStateMachine sm): base(sm) { }

    public Gun gun
    {
        get
        {
            return sm.gun;
        }
    }

    public override void Update(InputFrame input, GameObject obj)
    {
        base.Update(input, obj);

        if (input.PrimaryHold)
            gun.Shoot();

        if (input.DidScrollUp && gun.gunIndex == 0)
        {
            sm.NextState(new SwitchState(sm, gun.secondaryGun));
            gun.gunIndex = 1;
        }
        else if (input.DidScrollDown && gun.gunIndex == 1)
        {
            sm.NextState(new SwitchState(sm, gun.primaryGun));
            gun.gunIndex = 0;
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
            gun.maxSpread = gun.currentGun.maxSpread;
            gun.minSpread = gun.currentGun.minSpread;
            gun.spreadRate = gun.currentGun.spreadRate;
            gun.spreadRecoveryRate = gun.currentGun.spreadRecoveryRate;
        }
        else
        {
            gun.maxSpread = gun.currentGun.airMaxSpread;
            gun.minSpread = gun.currentGun.airMinSpread;
            gun.spreadRate = gun.currentGun.airSpreadRate;
            gun.spreadRecoveryRate = gun.currentGun.airSpreadRecoveryRate;
        }

    }
}
