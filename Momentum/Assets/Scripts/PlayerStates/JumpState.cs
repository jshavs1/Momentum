using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpState : LocomotionState
{
    public JumpState(PlayerStateMachine psm) : base(psm) { }
    public float force = 5.0f;

    public override void Enter(InputFrame input, GameObject obj)
    {
        base.Enter(input, obj);

        rigid.AddForce(LocomotionState.targetRotation * Vector3.up * force, ForceMode.Impulse);

        State nextState;
        if (isGrounded)
        {
            nextState = new GroundedState(psm);
        }
        else
        {
            nextState = new AirState(psm);
            ((AirState)nextState).canJump = false;
        }

        psm.NextState(nextState);
    }
}
