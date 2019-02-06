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
        Camera.main.GetComponent<CameraController>().MoveTo(4f);
    }

    public override void Update(InputFrame input, GameObject obj)
    {
        base.Update(input, obj);

        if (!input.SecondaryHold)
            sm.NextState(new HipFireState(sm));
    }

    public override void FixedUpdate(InputFrame input, GameObject obj)
    {
        base.FixedUpdate(input, obj);

        if (!isGrounded)
        {
            rc.AddDrag(0.04f, Direction.Vertical);
            rc.AddDrag(0.02f, Direction.Horizontal);
        }
    }

    public override void Exit(InputFrame input, GameObject obj)
    {
        base.Exit(input, obj);
        rc.multiplier = 1f;
        Camera.main.GetComponent<CameraController>().MoveBack();
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
