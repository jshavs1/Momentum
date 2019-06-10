using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Net;
using System.Linq;

[CreateAssetMenu(fileName = "New Hit Info", menuName = "Data/HitInfo")]
public class HitInfo: ScriptableObject
{
    private int senderId;
    private Team team;
    public float teammateHealthDelta;
    public float enemyHealthDelta;
    public float teammateKnockback;
    public float enemyKnockback;
    private Vector3 direction;

    public void SetId(int viewId)
    {
        this.senderId = viewId;
    }

    public int GetId()
    {
        return senderId;
    }

    public void SetTeam(Team team)
    {
        this.team = team;
    }

    public Team GetTeam()
    {
        return team;
    }

    public void SetDirection(Vector3 dir)
    {
        this.direction = dir;
    }

    public Vector3 GetDirection()
    {
        return direction;
    }

    public byte[] Serialize()
    {
        byte[] mem = new byte[4 * 8 + 1],
            teamBin = BitConverter.GetBytes((byte)team),
            teammateHealthDeltaBin = BitConverterFloat.HostToNetworkOrder(teammateHealthDelta),
            enemyHealthDeltaBin = BitConverterFloat.HostToNetworkOrder(enemyHealthDelta),
            teammateKnockbackBin = BitConverterFloat.HostToNetworkOrder(teammateKnockback),
            enemyKnockbackBin = BitConverterFloat.HostToNetworkOrder(enemyKnockback),
            senderIdBin = BitConverter.GetBytes(IPAddress.HostToNetworkOrder(senderId)),
            directionBin = direction.Serialize();

        teamBin.CopyTo(mem, 0);
        teammateHealthDeltaBin.CopyTo(mem, 1);
        enemyHealthDeltaBin.CopyTo(mem, 5);
        teammateKnockbackBin.CopyTo(mem, 9);
        enemyKnockbackBin.CopyTo(mem, 13);
        senderIdBin.CopyTo(mem, 17);
        directionBin.CopyTo(mem, 21);
        
        return mem;
    }

    public static HitInfo Deserialize(byte[] bin)
    {
        HitInfo data = new HitInfo();

        data.team = (Team)BitConverter.ToChar(bin, 0);
        data.teammateHealthDelta = BitConverterFloat.NetworkToHostOrder(bin, 1);
        data.enemyHealthDelta = BitConverterFloat.NetworkToHostOrder(bin, 5);
        data.teammateKnockback = BitConverterFloat.NetworkToHostOrder(bin, 9);
        data.enemyKnockback = BitConverterFloat.NetworkToHostOrder(bin, 13);
        data.senderId = IPAddress.NetworkToHostOrder(BitConverter.ToInt32(bin, 17));
        data.direction = Vector3.zero;
        data.direction.Deserialize(bin.Skip(21).ToArray());

        return data;
    }

    public HitInfo Clone()
    {
        HitInfo info = new HitInfo();
        info.teammateHealthDelta = teammateHealthDelta;
        info.enemyHealthDelta = enemyHealthDelta;
        info.teammateKnockback = teammateKnockback;
        info.enemyKnockback = enemyKnockback;

        return info;
    }
}
