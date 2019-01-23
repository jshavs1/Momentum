using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State
{
    public PlayerStateMachine psm;
    public string stateName { get { return GetType().Name; } }

    public State(PlayerStateMachine psm)
    {
        this.psm = psm;
    }

    public virtual void Enter(InputFrame input, GameObject obj) { Debug.Log("Entering State " + stateName); }
    public virtual void Update(InputFrame input, GameObject obj){ }
    public virtual void FixedUpdate(InputFrame input, GameObject obj) { }
    public virtual void Exit(InputFrame input, GameObject obj) { Debug.Log("Leaving State " + stateName); }
}
