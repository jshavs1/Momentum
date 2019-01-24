using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundedState : LocomotionState
{
    float acceleration = 4.0f;
    float maxSpeed = 12.0f;

    public GroundedState(PlayerStateMachine psm) : base(psm) { }

    public override void Enter(InputFrame input, GameObject obj)
    {
        base.Enter(input, obj);
        rigid = obj.GetComponent<Rigidbody>();
    }

    public override void Update(InputFrame input, GameObject obj)
    {
        base.Update(input, obj);

        if (input.JumpPress)
        {
            psm.NextState(new JumpState(psm));
            return;
        }
    }

    public override void FixedUpdate(InputFrame input, GameObject obj)
    {
        base.FixedUpdate(input, obj);

        if (rigid == null) { return; }

        Vector3 vel = new Vector3(rigid.velocity.x, 0f, rigid.velocity.z);
        Vector3 targetVel = new Vector3(input.x, 0f, input.y).normalized * maxSpeed;

        rigid.AddForce((LocomotionState.targetRotation * (targetVel - vel)) * acceleration);

        if (!isGrounded) { psm.NextState(new AirState(psm)); }
    }
}
