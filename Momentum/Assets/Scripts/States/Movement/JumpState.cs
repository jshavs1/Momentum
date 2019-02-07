using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpState : MovementState
{
    public JumpState(MovementSM sm) : base(sm) { }
    static float force = 10.0f;

    public override void Enter(InputFrame input, GameObject obj)
    {
        base.Enter(input, obj);

        Vector3 vel = rigid.velocity;
        if (vel.y < 0f)
        {
            vel.y = 0f;
            vel += targetRotation * Vector3.up * force;
            rigid.velocity = vel;
        }
        else
        {
            rigid.AddForce(targetRotation * Vector3.up * force, ForceMode.Impulse);
        }



        MovementState nextState;
        if (isGrounded)
        {
            if (input.Ability1Hold)
                nextState = new FlowState(sm);
            else
                nextState = new GroundState(sm);
        }
        else
        {
            if (input.Ability1Hold)
                nextState = new FlowState(sm);
            else
                nextState = new AirState(sm);
            canJump = false;
        }

        sm.NextState(nextState);
    }
}
