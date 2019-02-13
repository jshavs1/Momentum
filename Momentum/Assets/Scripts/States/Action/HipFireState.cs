using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HipFireState : GunState
{
    public HipFireState(ActionStateMachine sm): base(sm) { }

    public override void Update(InputFrame input, GameObject obj)
    {
        base.Update(input, obj);

        if (input.SecondaryHold)
            sm.NextState(new ADSState(sm));
    }

}
