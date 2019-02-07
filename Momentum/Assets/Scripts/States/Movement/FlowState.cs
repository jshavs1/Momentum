using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowState : MovementState
{
    public FlowState(MovementSM sm) : base(sm)
    {
        maxSpeed = 20.0f;
        acceleration = 12.0f;
    }

    static PhysicMaterial flowMaterial = new PhysicMaterial();

    public override void Enter(InputFrame input, GameObject obj)
    {
        base.Enter(input, obj);

        flowMaterial.bounciness = 0f;
        flowMaterial.staticFriction = 0.1f;
        flowMaterial.dynamicFriction = 0f;
        flowMaterial.frictionCombine = PhysicMaterialCombine.Minimum;
        collider.material = flowMaterial;
    }

    public override void Update(InputFrame input, GameObject obj)
    {
        base.Update(input, obj);

        if (!input.Ability1Hold)
        {
            MovementState nextState;
            if (isGrounded)
                nextState = new GroundState(sm);
            else
                nextState = new AirState(sm);

            sm.NextState(nextState);
        }
    }

    public override void FixedUpdate(InputFrame input, GameObject obj)
    {
        base.FixedUpdate(input, obj);

        AddMomemtum(input, obj);
    }

    public override void Exit(InputFrame input, GameObject obj)
    {
        base.Exit(input, obj);
        collider.material = null;
    }
}
