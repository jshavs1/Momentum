using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirFlowState : FlowState
{
    public AirFlowState(PlayerStateMachine psm) : base(psm){ }

    public override void Update(InputFrame input, GameObject obj)
    {
        base.Update(input, obj);

        if (input.JumpPress && canJump)
        {
            psm.NextLocomotionState(new JumpState(psm));
            return;
        }
        if (!input.Ability1Hold)
        {
            psm.NextLocomotionState(new AirState(psm));
        }
    }

    public override void FixedUpdate(InputFrame input, GameObject obj)
    {
        base.FixedUpdate(input, obj);
        AddMomemtum(input, obj);

        if (isGrounded) { psm.NextLocomotionState(new GroundedFlowState(psm)); }
    }
}
