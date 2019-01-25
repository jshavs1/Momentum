using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundedFlowState : FlowState
{
    public GroundedFlowState(PlayerStateMachine psm) : base(psm)
    {
        acceleration = 10.0f;
    }
    public override void Update(InputFrame input, GameObject obj)
    {
        base.Update(input, obj);

        if (input.JumpPress)
        {
            psm.NextLocomotionState(new JumpState(psm));
            return;
        }
        if (!input.Ability1Hold)
        {
            psm.NextLocomotionState(new GroundedState(psm));
        }
    }

    public override void FixedUpdate(InputFrame input, GameObject obj)
    {
        base.FixedUpdate(input, obj);

        AddMomemtum(input, obj);
    }
}
