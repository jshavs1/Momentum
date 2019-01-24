using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : MonoBehaviour
{
    public State currentState;
    public CameraController cc;
    public List<State> previousStates;
    public InputFrame currentInput;
    public LayerMask mask;
    public bool debug = false;
    private PhotonView pv;
    private int maxRecordSize = 3;

    public void Start()
    {
        pv = GetComponent<PhotonView>();
        if (!pv.IsMine) { return; }

        previousStates = new List<State>();
        NextState(new GroundedState(this));
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

        currentState.Update(currentInput, gameObject);
    }

    public void FixedUpdate()
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
        RecordState(currentState);

        currentState?.Exit(currentInput, gameObject);
        currentState = nextState;
        currentState.Enter(currentInput, gameObject);
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
