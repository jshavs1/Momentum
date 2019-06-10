using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody))]
public class Projectile : MonoBehaviour
{
    public float gravityMultiplier;
    protected Rigidbody rigid;

    float gravity = Mathf.Abs(Physics.gravity.y);
    // Start is called before the first frame update
    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        rigid.useGravity = false;
    }

    private void FixedUpdate()
    {
        rigid.AddForce(gravityMultiplier * Physics.gravity);   
    }

    public void Launch(Vector3 vel)
    {
        rigid.velocity = vel;
    }
}

public class ProjectileEvent : UnityEvent<Collision> { }

