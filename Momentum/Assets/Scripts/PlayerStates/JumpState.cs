using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpState : RigidbodyState
{
    public JumpState(PlayerStateMachine psm) : base(psm) { }
    public float force = 5.0f;

    public override void Enter(InputFrame input, GameObject obj)
    {
        base.Enter(input, obj);

        rigid.AddForce(Vector3.up * force, ForceMode.Impulse);

        AirState nextState = new AirState(psm);

        nextState.canJump = (psm.previousStates[0].GetType() != nextState.GetType());

        psm.NextState(nextState, input, obj);
    }
}
