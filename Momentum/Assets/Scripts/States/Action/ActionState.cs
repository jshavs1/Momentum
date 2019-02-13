using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionState : State
{
    public new ActionStateMachine sm;
    public ActionState(ActionStateMachine sm) : base(sm)
    {
        this.sm = sm;
    }
}
