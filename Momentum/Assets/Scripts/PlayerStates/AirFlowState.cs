using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirFlowState : FlowState
{
    public AirFlowState(PlayerStateMachine psm) : base(psm) { }

    public bool canJump = true;
    float acceleration = 5.0f;

    public override void Update(InputFrame input, GameObject obj)
    {
        base.Update(input, obj);

        if (input.JumpPress && canJump)
        {
            psm.NextState(new JumpState(psm));
            return;
        }
        if (!input.Ability1Hold)
        {
            psm.NextState(new AirState(psm));
        }
    }

    public override void FixedUpdate(InputFrame input, GameObject obj)
    {
        base.FixedUpdate(input, obj);
        Vector3 force = targetRotation * new Vector3(input.x, 0f, input.y) * acceleration;

        if (rigid.velocity.magnitude < maxSpeed || Vector3.Dot(force, rigid.velocity) < 0f)
            rigid.AddForce(force);

        if (isGrounded) { psm.NextState(new GroundedFlowState(psm)); }
    }
}
