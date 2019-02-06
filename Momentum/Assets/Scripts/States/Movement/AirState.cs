using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirState : MovementState
{
    public AirState(MovementSM sm) : base(sm)
    {
        acceleration = 10.0f;
        maxSpeed = 10.0f;
    }

    public override void Enter(InputFrame input, GameObject obj)
    {
        base.Enter(input, obj);
    }

    public override void Update(InputFrame input, GameObject obj)
    {
        base.Update(input, obj);

        if (input.Ability1Hold)
        {
            sm.NextState(new FlowState(sm));
        }
    }

    public override void FixedUpdate(InputFrame input, GameObject obj)
    {
        base.FixedUpdate(input, obj);

        Vector3 horVel = rigid.velocity;
        horVel.y = 0f;
        if (horVel.magnitude > maxSpeed)
            rc.AddDrag(0.02f, Direction.Horizontal);

        AddMomemtum(input, obj);

        if (isGrounded) { sm.NextState(new GroundState(sm)); }
    }
}
