using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : MonoBehaviour
{
    private PhotonView pv;
    public CameraController cc;

    public LocomotionState locomotionState;
    public State actionState;
    
    public InputFrame currentInput;
    public LayerMask mask, actionMask;

    public bool debug = false;
    
    public void Start()
    {
        pv = GetComponent<PhotonView>();
        if (!pv.IsMine) { return; }

        NextLocomotionState(new GroundedState(this));
    }

    public void Update()
    {
        if (!pv.IsMine) { return; }
        currentInput = InputFrame.GetFrame();

        if (cc != null)
        {
            Vector2 relInput = cc.InputToCameraSpace(new Vector2(currentInput.x, currentInput.y));
            currentInput.x = relInput.x;
            currentInput.y = relInput.y;
        }

        locomotionState.Update(currentInput, gameObject);
    }

    public void FixedUpdate()
    {
        if (!pv.IsMine) { return; }
        locomotionState.FixedUpdate(currentInput, gameObject);
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (!pv.IsMine) { return; }
        locomotionState.OnCollisionEnter(collision, currentInput, gameObject);
    }

    public void OnCollisionExit(Collision collision)
    {
        if (!pv.IsMine) { return; }
        locomotionState.OnCollisionExit(collision, currentInput, gameObject);
    }

    public void OnCollisionStay(Collision collision)
    {
        if (!pv.IsMine) { return; }
        locomotionState.OnCollisionStay(collision, currentInput, gameObject);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (!pv.IsMine) { return; }
        locomotionState.OnTriggerEnter(other, currentInput, gameObject);
    }

    public void OnTriggerExit(Collider other)
    {
        if (!pv.IsMine) { return; }
        locomotionState.OnTriggerExit(other, currentInput, gameObject);
    }

    public void OnTriggerStay(Collider other)
    {
        if (!pv.IsMine) { return; }
        locomotionState.OnTriggerStay(other, currentInput, gameObject);
    }

    private void NextState<T>(ref T currentState, T nextState) where T: State
    {
        if (debug)
            Debug.Log((currentState != null ? currentState.GetType().Name : "None") + " -> " + nextState.GetType().Name);
        currentState?.Exit(currentInput, gameObject);
        currentState = nextState;
        currentState.Enter(currentInput, gameObject);
    }

    public void NextLocomotionState(LocomotionState nextState)
    {
        NextState(ref locomotionState, nextState);
    }

    public void NextActionState(ActionState nextState)
    {
        NextState(ref actionState, nextState);
    }
}
