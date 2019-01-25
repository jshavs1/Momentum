using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : ActionState
{
    public IdleState(PlayerStateMachine psm) : base(psm) { }

    public override void Update(InputFrame input, GameObject obj)
    {
        base.Update(input, obj);

        if (input.PrimaryHold)
        {
            psm.NextActionState(new ShootState(psm));
            return;
        }
    }

}
