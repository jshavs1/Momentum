using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiplayerNetworkManager : MonoBehaviourPunCallbacks
{

    public static MultiplayerNetworkManager Instance;

    public List<RoomInfo> roomList = new List<RoomInfo>();
    
    void Start()
    {
        if (Instance == null) { Instance = this; }
        else { Destroy(this); return; }
        DontDestroyOnLoad(this);

        ConnectToMaster();
    }

    void ConnectToMaster()
    {
        PhotonNetwork.OfflineMode = false;
        PhotonNetwork.NickName = "Jack";
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.GameVersion = "0.0.1";


        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
        Debug.Log("Connected to Master");
        
        Debug.Log("Attempting to join Lobby");

        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        base.OnJoinedLobby();

        Debug.Log("Connected to Lobby");
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        Debug.Log("Joined Room " + PhotonNetwork.CurrentRoom.Name);

        if (PhotonNetwork.IsMasterClient) {
            PhotonNetwork.LoadLevel("Test");
        }
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        base.OnJoinRandomFailed(returnCode, message);
        Debug.Log(message);
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        base.OnDisconnected(cause);
        Debug.Log("Disconnected from Master");
    }

    public void JoinRoom(string name)
    {
        Debug.Log("Joining Room " + name);
        PhotonNetwork.JoinRoom(name);
    }

    public void CreateRoom(string name)
    {
        Debug.Log("Creating Room " + name);
        PhotonNetwork.CreateRoom(name, new RoomOptions() { MaxPlayers = 8 });
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        base.OnRoomListUpdate(roomList);
        Debug.Log("Room list updated");
        this.roomList = roomList;
    }
}
