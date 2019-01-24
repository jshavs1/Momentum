using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowState : LocomotionState
{
    public FlowState(PlayerStateMachine psm) : base(psm) { }

    static PhysicMaterial flowMaterial = new PhysicMaterial();
    public static float maxSpeed = 15f;

    public override void Enter(InputFrame input, GameObject obj)
    {
        base.Enter(input, obj);

        flowMaterial.bounciness = 0.7f;
        flowMaterial.staticFriction = 0.1f;
        flowMaterial.dynamicFriction = 0f;
        flowMaterial.frictionCombine = PhysicMaterialCombine.Minimum;
        collider.material = flowMaterial;
    }

    public override void Exit(InputFrame input, GameObject obj)
    {
        base.Exit(input, obj);
        collider.material = null;
    }
}
