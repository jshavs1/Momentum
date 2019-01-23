using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MultiplayerNetworkManager : MonoBehaviourPunCallbacks
{

    public static MultiplayerNetworkManager Instance;
    
    void Start()
    {
        if (Instance == null) { Instance = this; }
        else { Destroy(this); }
        DontDestroyOnLoad(this);

        ConnectToMaster();
    }

    void ConnectToMaster()
    {
        PhotonNetwork.OfflineMode = false;
        PhotonNetwork.NickName = "Jack";
        PhotonNetwork.GameVersion = "0.0.1";


        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
        Debug.Log("Connected to Master");

        Debug.Log("Attempting to join room");
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        Debug.Log("Joined Room " + PhotonNetwork.CurrentRoom.Name);

        SceneManager.LoadScene("Test");
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        base.OnJoinRandomFailed(returnCode, message);
        Debug.Log(message);
        CreateRoom(null);
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        base.OnDisconnected(cause);
        Debug.Log("Disconnected from Master");
    }

    public void CreateRoom(string name)
    {
        if (!PhotonNetwork.IsConnected) { return; }

        Debug.Log("Creating room: " + name);
        PhotonNetwork.CreateRoom(name, new RoomOptions() { MaxPlayers = 8 });
    }


}
