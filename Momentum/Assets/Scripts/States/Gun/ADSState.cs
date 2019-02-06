using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ADSState : GunState
{
    public ADSState(GunSM sm) : base(sm) { }

    public override void Enter(InputFrame input, GameObject obj)
    {
        base.Enter(input, obj);
        rc.multiplier = sm.gunProfile.ADSMovementSpeed;
        Camera.main.GetComponent<CameraController>().distanceAway -= 3f;
    }

    public override void Update(InputFrame input, GameObject obj)
    {
        base.Update(input, obj);

        if (!isGrounded || !input.SecondaryHold)
            sm.NextState(new HipFireState(sm));
    }

    public override void Exit(InputFrame input, GameObject obj)
    {
        base.Exit(input, obj);
        rc.multiplier = 1f;
        Camera.main.GetComponent<CameraController>().distanceAway += 3f;
    }

    public override void SetGunParams()
    {
        base.SetGunParams();

        sm.spreadRate = sm.spreadRate * (1f - sm.gunProfile.ADSAccuracy);
        sm.spreadRecoveryRate = sm.spreadRecoveryRate * (1f - sm.gunProfile.ADSAccuracy);
        sm.maxSpread = sm.maxSpread - (sm.maxSpread - sm.minSpread) * sm.gunProfile.ADSAccuracy;
        sm.minSpread = sm.minSpread * (1f - sm.gunProfile.ADSAccuracy);
    }
}
