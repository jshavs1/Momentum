using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkPlayer : MonoBehaviour
{
    private PhotonView PV;
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        PV = GetComponent<PhotonView>();

        if (!PV.IsMine) { return; }

        Debug.Log("Spawning Player!");
        player = MatchManager.Instance.SpawnPlayer();
    }
}


