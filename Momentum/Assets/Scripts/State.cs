using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State
{
    public StateMachine sm;
    public string stateName { get { return GetType().Name; } }

    protected GroundDetection gd;
    protected RigidbodyController rc;


    public State(StateMachine sm)
    {
        this.sm = sm;
    }

    public virtual void Enter(InputFrame input, GameObject obj) { Init(input, obj); }
    public virtual void Update(InputFrame input, GameObject obj){ }
    public virtual void FixedUpdate(InputFrame input, GameObject obj) { }
    public virtual void Exit(InputFrame input, GameObject obj) { }
    public virtual void OnCollisionEnter(Collision col, InputFrame input, GameObject obj) { }
    public virtual void OnCollisionExit(Collision col, InputFrame input, GameObject obj) { }
    public virtual void OnCollisionStay(Collision col, InputFrame input, GameObject obj) { }
    public virtual void OnTriggerEnter(Collider other, InputFrame input, GameObject obj) { }
    public virtual void OnTriggerExit(Collider other, InputFrame input, GameObject obj) { }
    public virtual void OnTriggerStay(Collider other, InputFrame input, GameObject obj) { }


    private void Init(InputFrame input, GameObject obj)
    {
        gd = obj.GetComponent<GroundDetection>() ?? obj.AddComponent<GroundDetection>();
        rc = obj.GetComponent<RigidbodyController>();
    }

    public bool isGrounded
    {
        get
        {
            return gd?.isGrounded ?? true;
        }
    }

    public Rigidbody rigid
    {
        get
        {
            return rc.rigid;
        }
    }

}
