using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpState : MovementState
{
    public JumpState(MovementSM sm) : base(sm) { }
    static float force = 6.0f;

    public override void Enter(InputFrame input, GameObject obj)
    {
        base.Enter(input, obj);

        rigid.AddForce(targetRotation * Vector3.up * force, ForceMode.Impulse);

        MovementState nextState;
        if (isGrounded)
        {
            if (input.Ability1Hold)
                nextState = new FlowState(sm);
            else
                nextState = new GroundState(sm);

            nextState.canJump = true;
        }
        else
        {
            if (input.Ability1Hold)
                nextState = new FlowState(sm);
            else
                nextState = new AirState(sm);

            nextState.canJump = false;
        }

        sm.NextState(nextState);
    }
}
