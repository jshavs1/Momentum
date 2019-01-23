using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundedState : RigidbodyState
{
    float acceleration = 0.85f;

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
            psm.NextState(new JumpState(psm), input, obj);
            return;
        }
    }

    public override void FixedUpdate(InputFrame input, GameObject obj)
    {
        base.Update(input, obj);

        float newX = Mathf.Lerp(rigid.velocity.x, input.x, acceleration);
        float newZ = Mathf.Lerp(rigid.velocity.z, input.y, acceleration);

        rigid.velocity.Set(newX, rigid.velocity.y, newZ);
    }

}
