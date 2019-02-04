using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirState : LocomotionState
{
    public AirState(PlayerStateMachine psm) : base(psm)
    {
        acceleration = 10.0f;
        maxSpeed = 5.0f;
    }

    public override void Enter(InputFrame input, GameObject obj)
    {
        base.Enter(input, obj);
        obj.GetComponent<Gun>()?.SetState(false);
    }

    public override void Update(InputFrame input, GameObject obj)
    {
        base.Update(input, obj);

        if (input.JumpPress && canJump)
        {
            psm.NextLocomotionState(new JumpState(psm));
            return;
        }
        if (input.Ability1Hold)
        {
            psm.NextLocomotionState(new AirFlowState(psm));
        }
    }

    public override void FixedUpdate(InputFrame input, GameObject obj)
    {
        base.FixedUpdate(input, obj);

        AddMomemtum(input, obj);

        if (isGrounded) { psm.NextLocomotionState(new GroundedState(psm)); }
    }
}
