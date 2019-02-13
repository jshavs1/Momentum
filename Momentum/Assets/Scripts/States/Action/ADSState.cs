using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ADSState : GunState
{
    public ADSState(ActionStateMachine sm) : base(sm) { }

    public override void Enter(InputFrame input, GameObject obj)
    {
        base.Enter(input, obj);
        rc.multiplier = gun.currentGun.ADSMovementSpeed;
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

        gun.spreadRate = gun.spreadRate * (1f - gun.currentGun.ADSAccuracy);
        gun.spreadRecoveryRate = gun.spreadRecoveryRate * (1f - gun.currentGun.ADSAccuracy);
        gun.maxSpread = gun.maxSpread - (gun.maxSpread - gun.minSpread) * gun.currentGun.ADSAccuracy;
        gun.minSpread = gun.minSpread * (1f - gun.currentGun.ADSAccuracy);
    }
}
