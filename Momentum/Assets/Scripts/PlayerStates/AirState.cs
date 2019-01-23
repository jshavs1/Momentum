using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirState : RigidbodyState
{
    public AirState(PlayerStateMachine psm) : base(psm) { }

    float acceleration = 5.0f;
    float maxSpeed = 5.0f;

    public override void FixedUpdate(InputFrame input, GameObject obj)
    {
        base.Update(input, obj);

        Vector3 vel = rigid.velocity;

        if (input.x > 0f)
        {
            if (vel.x < maxSpeed)
            {
                rigid.AddForce(Vector3.right * input.x * maxSpeed);
            }
        }
        if (input.x < 0f)
        {
            if (vel.x > maxSpeed)
            {
                rigid.AddForce(Vector3.right * input.x * maxSpeed);
            }
        }

        if (input.y > 0f)
        {
            if (vel.z < maxSpeed)
            {
                rigid.AddForce(Vector3.forward * input.y * maxSpeed);
            }
        }
        if (input.y < 0f)
        {
            if (vel.z > maxSpeed)
            {
                rigid.AddForce(Vector3.forward * input.y * maxSpeed);
            }
        }

    }
}
