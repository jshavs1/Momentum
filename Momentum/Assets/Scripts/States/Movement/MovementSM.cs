using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementSM : StateMachine
{
    public LayerMask collisionMask;

    public override State StartingState()
    {
        return new GroundState(this);
    }
}
