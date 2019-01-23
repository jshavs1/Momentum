using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine
{
    public State currentState;

    public void Update(InputFrame input, GameObject obj)
    {
        currentState.Update(input, obj);
    }

    public void FixedUpdate(InputFrame input, GameObject obj)
    {
        currentState.FixedUpdate(input, obj);
    }

    public void NextState(State nextState, InputFrame input, GameObject obj)
    {
        currentState.Exit(input, obj);
        currentState = nextState;
        currentState.Enter(input, obj);
    }

}
