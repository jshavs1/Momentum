using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundedState : LocomotionState
{
    public GroundedState(PlayerStateMachine psm) : base(psm)
    {
        acceleration = 5.0f;
        maxSpeed = 12.0f;
    }

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
            psm.NextLocomotionState(new JumpState(psm));
            return;
        }
        if (input.Ability1Hold)
        {
            psm.NextLocomotionState(new GroundedFlowState(psm));
            return;
        }
    }

    public override void FixedUpdate(InputFrame input, GameObject obj)
    {
        base.FixedUpdate(input, obj);

        if (rigid == null) { return; }

        Vector3 vel = new Vector3(rigid.velocity.x, 0f, rigid.velocity.z);
        Vector3 targetVel = new Vector3(input.x, 0f, input.y).normalized * maxSpeed;

        rigid.AddForce((targetRotation * (targetVel - vel)) * acceleration);

        if (!isGrounded) { psm.NextLocomotionState(new AirState(psm)); }
    }
}
