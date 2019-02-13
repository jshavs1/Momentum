using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrusterState : AbilityState
{
    public new ThrusterAbilityProfile ability;
    public float remainingFuel;

    public ThrusterState(AbilityStateMachine sm): base(sm) { }

    public override void Update(InputFrame input, GameObject obj)
    {
        base.Update(input, obj);

        Debug.Log("Remaining Fuel: " + remainingFuel);
    }

    public override void FixedUpdate(InputFrame input, GameObject obj)
    {
        base.FixedUpdate(input, obj);

        if (input.JumpHold)
        {
            AddThrust();
            remainingFuel -= Time.fixedDeltaTime;
        }

        if (remainingFuel <= 0f)
            sm.NextState(new IdleState(sm));
    }

    private void AddThrust()
    {
        Vector3 vel = rigid.velocity;
        float relativeThrust = Mathf.Max(Mathf.Max(ability.maxYVel - vel.y, 0f) / ability.maxYVel, 1f);

        rigid.AddForce(Vector3.up * ability.thrust * relativeThrust);
    }
}
