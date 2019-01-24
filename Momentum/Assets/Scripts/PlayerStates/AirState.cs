﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirState : LocomotionState
{
    public AirState(PlayerStateMachine psm) : base(psm) { }

    float acceleration = 8.0f;
    float maxSpeed = 5.0f;
    public bool canJump = true;

    public override void Update(InputFrame input, GameObject obj)
    {
        base.Update(input, obj);

        if (input.JumpPress && canJump)
        {
            psm.NextState(new JumpState(psm));
            return;
        }
        if (input.Ability1Hold)
        {
            psm.NextState(new AirFlowState(psm));
        }
    }

    public override void FixedUpdate(InputFrame input, GameObject obj)
    {
        base.FixedUpdate(input, obj);

        Vector3 force = targetRotation * new Vector3(input.x, 0f, input.y) * acceleration;

        if (rigid.velocity.magnitude < maxSpeed || Vector3.Dot(force, rigid.velocity) < 0f)
            rigid.AddForce(force);

        if (isGrounded) { psm.NextState(new GroundedState(psm)); }
    }
}
