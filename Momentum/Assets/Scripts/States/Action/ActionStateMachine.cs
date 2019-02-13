using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Gun))]
public class ActionStateMachine : StateMachine
{
    public Gun gun;

    public void Awake()
    {
        gun = GetComponent<Gun>();
    }

    public override State StartingState()
    {
        return new HipFireState(this);
    }

}
