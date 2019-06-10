using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System;
using System.Net;

public class PlayerInfo : MonoBehaviour
{
    PlayerInfoData data;
    // Start is called before the first frame update
    void Start()
    {
        data = new PlayerInfoData();
        if (GetComponent<PhotonView>().IsMine)
        {
            data.id = GetComponent<PhotonView>().ViewID;
            data.team = (Team) PhotonNetwork.LocalPlayer.CustomProperties["team"];
        }
    }
}

public class PlayerInfoData
{
    public int id;
    public Team team;

    public byte[] Serialize()
    {
        byte[] mem = new byte[4 + 1],
        idMem = BitConverter.GetBytes(IPAddress.HostToNetworkOrder(id)),
        teamMem = BitConverter.GetBytes((byte)team);

        idMem.CopyTo(mem, 0);
        teamMem.CopyTo(mem, idMem.Length);

        return mem;
    }

    public static PlayerInfoData Deserialize(byte[] bin)
    {
        PlayerInfoData data = new PlayerInfoData();
        data.id = IPAddress.NetworkToHostOrder(BitConverter.ToInt32(bin, 0));
        data.team = (Team)BitConverter.ToChar(bin, 4);

        return data;
    }
}
