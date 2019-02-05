using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundState : MovementState
{
    public GroundState(MovementSM sm) : base(sm)
    {
        acceleration = 7.0f;
        maxSpeed = 12.0f;
    }

    public override void Enter(InputFrame input, GameObject obj)
    {
        base.Enter(input, obj);
    }

    public override void Update(InputFrame input, GameObject obj)
    {
        base.Update(input, obj);

        if (input.Ability1Hold)
        {
            sm.NextState(new FlowState(sm));
            return;
        }
    }

    public override void FixedUpdate(InputFrame input, GameObject obj)
    {
        base.FixedUpdate(input, obj);

        if (rigid == null) { return; }

        Vector3 vel = new Vector3(rigid.velocity.x, 0f, rigid.velocity.z);
        Vector3 targetVel = new Vector3(input.x, 0f, input.y).normalized * maxSpeed;

        rc.AddForce((targetRotation * (targetVel - vel)) * acceleration);

        if (!isGrounded) { sm.NextState(new AirState(sm)); }
    }
}
