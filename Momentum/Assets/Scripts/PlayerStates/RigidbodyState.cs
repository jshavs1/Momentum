using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidbodyState : State
{
    public Rigidbody rigid;

    public RigidbodyState(PlayerStateMachine psm) : base(psm) { }

    public override void Enter(InputFrame input, GameObject obj)
    {
        base.Enter(input, obj);
        rigid = obj.GetComponent<Rigidbody>();
    }
}
