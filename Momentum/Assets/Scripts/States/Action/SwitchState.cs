using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchState : GunState
{
    GunProfile nextGun;
    float remainingSwitchTime;

    public SwitchState(ActionStateMachine sm, GunProfile nextGun) : base(sm)
    {
        this.nextGun = nextGun;
    }

    public override void Enter(InputFrame input, GameObject obj)
    {
        base.Enter(input, obj);

        remainingSwitchTime = nextGun.switchToTime;
        gun.currentGun = nextGun;
        gun.ResetSpread();
        SetGunParams();
    }

    public override void Update(InputFrame input, GameObject obj)
    {
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
        if (remainingSwitchTime <= 0f)
            sm.NextState(new HipFireState(sm));
        else
            remainingSwitchTime -= Time.fixedDeltaTime;
    }
}
