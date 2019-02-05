using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateMachine : MonoBehaviour
{
    private PhotonView pv;

    public State currentState, previousState;

    public InputFrame currentInput;

    public bool debug;

    public abstract State StartingState();

    public void Start()
    {
        pv = GetComponent<PhotonView>();
        if (!pv.IsMine) { return; }

        NextState(StartingState());
    }

    public virtual void Update()
    {
        if (!pv.IsMine) { return; }
        currentInput = InputFrame.GetFrame();

        currentState.Update(currentInput, gameObject);
    }

    public virtual void FixedUpdate()
    {
        if (!pv.IsMine) { return; }
        currentState.FixedUpdate(currentInput, gameObject);
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (!pv.IsMine) { return; }
        currentState.OnCollisionEnter(collision, currentInput, gameObject);
    }

    public void OnCollisionExit(Collision collision)
    {
        if (!pv.IsMine) { return; }
        currentState.OnCollisionExit(collision, currentInput, gameObject);
    }

    public void OnCollisionStay(Collision collision)
    {
        if (!pv.IsMine) { return; }
        currentState.OnCollisionStay(collision, currentInput, gameObject);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (!pv.IsMine) { return; }
        currentState.OnTriggerEnter(other, currentInput, gameObject);
    }

    public void OnTriggerExit(Collider other)
    {
        if (!pv.IsMine) { return; }
        currentState.OnTriggerExit(other, currentInput, gameObject);
    }

    public void OnTriggerStay(Collider other)
    {
        if (!pv.IsMine) { return; }
        currentState.OnTriggerStay(other, currentInput, gameObject);
    }

    public void NextState(State nextState)
    {
        if (debug)
            Debug.Log(GetType() + ": " + (currentState != null ? currentState.GetType().Name : "None") + " -> " + nextState.GetType().Name);

        previousState = currentState;
        currentState?.Exit(currentInput, gameObject);
        currentState = nextState;
        currentState.Enter(currentInput, gameObject);
    }
}

