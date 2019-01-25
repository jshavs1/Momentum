using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootState : ActionState
{
    private Gun gun;

    public ShootState(PlayerStateMachine psm) : base(psm) { }

    public override void Enter(InputFrame input, GameObject obj)
    {
        base.Enter(input, obj);
        gun = obj.GetComponent<Gun>();
    }

    public override void Update(InputFrame input, GameObject obj)
    {
        base.Update(input, obj);

        if (input.PrimaryHold)
        {
            gun?.Shoot();
        }
        else
        {
            psm.NextActionState(new IdleState(psm));
        }
    }
}
