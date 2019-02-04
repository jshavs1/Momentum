using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class RigidbodyController : MonoBehaviour
{
    Rigidbody rigid;

    public float multiplier = 1f;

    public Vector3 velocity
    {
        get
        {
            return rigid.velocity;
        }
        set
        {
            rigid.velocity = value;
        }
    }

    // Start is called before the first frame update
    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
    }

    public void AddForce(Vector3 force, ForceMode mode = ForceMode.Force)
    {
        rigid.AddForce(force * multiplier, mode);
    }
}
