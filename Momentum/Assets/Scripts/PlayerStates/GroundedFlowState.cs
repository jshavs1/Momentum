using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundedFlowState : FlowState
{
    public GroundedFlowState(PlayerStateMachine psm) : base(psm) { }

    float acceleration = 10.0f;

    public override void Update(InputFrame input, GameObject obj)
    {
        base.Update(input, obj);

        if (input.JumpPress)
        {
            psm.NextState(new JumpState(psm));
            return;
        }
        if (!input.Ability1Hold)
        {
            psm.NextState(new GroundedState(psm));
        }
    }

    public override void FixedUpdate(InputFrame input, GameObject obj)
    {
        base.FixedUpdate(input, obj);

        Vector3 force = targetRotation * new Vector3(input.x, 0f, input.y) * acceleration;
        
        if (rigid.velocity.magnitude < maxSpeed || Vector3.Dot(force, rigid.velocity) < 0f)
            rigid.AddForce(force);
    }
}
