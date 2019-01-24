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

    public virtual void Enter(InputFrame input, GameObject obj) { if (psm.debug) { Debug.Log("Entering State " + stateName); } }
    public virtual void Update(InputFrame input, GameObject obj){ }
    public virtual void FixedUpdate(InputFrame input, GameObject obj) { }
    public virtual void Exit(InputFrame input, GameObject obj) { if (psm.debug) { Debug.Log("Leaving State " + stateName); } }
    public virtual void OnCollisionEnter(Collision col, InputFrame input, GameObject obj) { }
    public virtual void OnCollisionExit(Collision col, InputFrame input, GameObject obj) { }
    public virtual void OnCollisionStay(Collision col, InputFrame input, GameObject obj) { }
    public virtual void OnTriggerEnter(Collider other, InputFrame input, GameObject obj) { }
    public virtual void OnTriggerExit(Collider other, InputFrame input, GameObject obj) { }
    public virtual void OnTriggerStay(Collider other, InputFrame input, GameObject obj) { }


}
