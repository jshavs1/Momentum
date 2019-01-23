using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchManager : MonoBehaviour
{
    public static MatchManager Instance;
    public Transform[] spawnPoints;


    // Start is called before the first frame update
    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
            StartCoroutine(InstantiateNetworkPlayer());
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private IEnumerator InstantiateNetworkPlayer()
    {
        yield return new WaitForSecondsRealtime(2.0f);
        Debug.Log("Instantiating NetworkPlayer");
        PhotonNetwork.Instantiate("NetworkPlayer", Vector3.zero, Quaternion.identity, 0);
    }

    public GameObject SpawnPlayer()
    {
        int spawnIndex = (int) Random.Range(0, spawnPoints.Length - 1);
        Transform spawnTransform = spawnPoints[spawnIndex];

        return PhotonNetwork.Instantiate("Player", spawnTransform.position, spawnTransform.rotation, 0);
    }
}
