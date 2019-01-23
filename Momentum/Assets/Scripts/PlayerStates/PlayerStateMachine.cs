using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine
{
    public State currentState;
    public List<State> previousStates;
    public bool debug = false;
    private int maxRecordSize = 3;

    public PlayerStateMachine()
    {
        currentState = new GroundedState(this);
        previousStates = new List<State>();
    }

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
        RecordState(currentState);

        currentState.Exit(input, obj);
        currentState = nextState;
        currentState.Enter(input, obj);
    }

    private void RecordState(State state)
    {
        if (previousStates.Count >= maxRecordSize)
        {
            previousStates.RemoveAt(previousStates.Count - 1);
        }
        previousStates.Insert(0, state);
    }

}
