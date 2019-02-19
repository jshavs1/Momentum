using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

[RequireComponent(typeof(Rigidbody))]
public class RigidbodyController : MonoBehaviour
{
    public Rigidbody rigid;

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

    public void AddDrag(float drag, Direction dir)
    {
        Vector3 vel = rigid.velocity;
        if (dir != Direction.Vertical)
        {
            vel.x *= (1f - drag);
            vel.z *= (1f - drag);
        }
        if (dir != Direction.Horizontal)
        {
            vel.y *= (1f - drag);
        }
        rigid.velocity = vel;
    }

    public void TakeKnockback(float knockback, Vector3 dir)
    {
        GetComponent<PhotonView>()?.RPC("TakeKnockbackRPC", RpcTarget.All, knockback, dir);
    }

    [PunRPC]
    private void TakeKnockbackRPC(float knockback, Vector3 dir)
    {
        rigid.AddForce(dir * knockback, ForceMode.Impulse);
    }
}


public enum Direction
{
    Horizontal,
    Vertical,
    All
}
