using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class HitMask: MonoBehaviour, IPunObservable
{
    private int mask;

    void Awake()
    {
        Initialize();
    }

    void Initialize()
    {
        if (GetComponent<PhotonView>().IsMine)
        {
            mask = 0;
            SetTeam((Team) PhotonNetwork.LocalPlayer.CustomProperties["team"]);
        }
    }

    #region MASK INFO
    /*
     *  Position 0 - Team 
     *  Position 1 - Invulnerability
    */
    #endregion

    private void ResetBitAt(int position)
    {
        mask &= ~(1 << position);
    }

    private void SetBitAt(int position)
    {
        mask |= 1 << position; 
    }

    private void SetBitValueAt(int position, int value)
    {
        if (value == 0)
        {
            ResetBitAt(position);
        }
        else if (value == 0)
        {
            SetBitAt(position);
        }
        else
        {
            Debug.Log("Bit value must be 0 or 1");
        }
    }

    private int GetBitValue(int position)
    {
        return (mask >> position) & 1;
    }

    public void SetTeam(Team team)
    {
        SetBitValueAt(0, (int)team);
    }

    public Team GetTeam()
    {
        return (Team)GetBitValue(0);
    }

    public void SetInvulerability(bool isInvulnerable)
    {
        SetBitValueAt(1, isInvulnerable ? 1 : 0);
    }

    public bool IsInvulnerable()
    {
        return GetBitValue(1) == 1 ? true : false;
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if(stream.IsWriting)
        {
            stream.Serialize(ref mask);
        }
        else if (stream.IsReading)
        {
            mask = (int) stream.ReceiveNext();
        }
    }
}
