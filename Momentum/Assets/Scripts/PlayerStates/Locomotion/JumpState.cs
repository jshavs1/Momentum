using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpState : LocomotionState
{
    public JumpState(PlayerStateMachine psm) : base(psm) { }
    static float force = 5.0f;

    public override void Enter(InputFrame input, GameObject obj)
    {
        base.Enter(input, obj);

        rigid.AddForce(targetRotation * Vector3.up * force, ForceMode.Impulse);

        LocomotionState nextState;
        if (isGrounded)
        {
            if (input.Ability1Hold)
                nextState = new GroundedFlowState(psm);
            else
                nextState = new GroundedState(psm);
        }
        else
        {
            if (input.Ability1Hold)
            {
                nextState = new AirFlowState(psm);
                nextState.canJump = false;
            }
            else
            {
                nextState = new AirState(psm);
                nextState.canJump = false;
            }
            
        }

        psm.NextLocomotionState(nextState);
    }
}
