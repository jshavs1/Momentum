using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirState : RigidbodyState
{
    public AirState(PlayerStateMachine psm) : base(psm) { }

    float acceleration = 7.0f;
    float maxSpeed = 5.0f;
    public bool canJump;

    public override void Update(InputFrame input, GameObject obj)
    {
        base.Update(input, obj);

        if (input.JumpPress && canJump)
        {
            psm.NextState(new JumpState(psm), input, obj);
        }
    }

    public override void FixedUpdate(InputFrame input, GameObject obj)
    {
        base.FixedUpdate(input, obj);

        Vector3 vel = rigid.velocity;

        if (input.x > 0f)
        {
            if (vel.x < maxSpeed)
            {
                rigid.AddForce(Vector3.right * input.x * acceleration);
            }
        }
        if (input.x < 0f)
        {
            if (vel.x > -maxSpeed)
            {
                rigid.AddForce(Vector3.right * input.x * acceleration);
            }
        }

        if (input.y > 0f)
        {
            if (vel.z < maxSpeed)
            {
                rigid.AddForce(Vector3.forward * input.y * acceleration);
            }
        }
        if (input.y < 0f)
        {
            if (vel.z > -maxSpeed)
            {
                rigid.AddForce(Vector3.forward * input.y * acceleration);
            }
        }

    }
}
