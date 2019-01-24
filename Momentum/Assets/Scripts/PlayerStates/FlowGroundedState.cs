using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowGroundedState : RigidbodyState
{
    Collider col;

    public FlowGroundedState(PlayerStateMachine psm) : base(psm) { }

    public override void Enter(InputFrame input, GameObject obj)
    {
        base.Enter(input, obj);

        col = obj.GetComponent<Collider>();
    }

    public override void FixedUpdate(InputFrame input, GameObject obj)
    {
        base.FixedUpdate(input, obj);

        
    }

}
