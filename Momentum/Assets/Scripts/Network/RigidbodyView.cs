using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(PhotonView))]
public class RigidbodyView : MonoBehaviour, IPunObservable
{
    PhotonView pv;
    Rigidbody rigid;
    Vector3 networkPosition;
    Quaternion networkRotation;

    public void OnEnable()
    {
        rigid = GetComponent<Rigidbody>();
        pv = GetComponent<PhotonView>();
    }

    public void FixedUpdate()
    {
        if (!pv.IsMine)
        {
            rigid.position = Vector3.MoveTowards(rigid.position, networkPosition, Time.fixedDeltaTime);
            rigid.rotation = Quaternion.RotateTowards(rigid.rotation, networkRotation, Time.fixedDeltaTime * 100.0f);
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(rigid.position);
            stream.SendNext(rigid.rotation);
            stream.SendNext(rigid.velocity);
        }
        else
        {
            networkPosition = (Vector3)stream.ReceiveNext();
            networkRotation = (Quaternion)stream.ReceiveNext();
            rigid.velocity = (Vector3)stream.ReceiveNext();

            float lag = Mathf.Abs((float)(PhotonNetwork.Time - info.SentServerTime));
            networkPosition += (rigid.velocity * lag);
        }
    }
}
