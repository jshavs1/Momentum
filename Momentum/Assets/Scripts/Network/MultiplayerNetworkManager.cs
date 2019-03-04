using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class MultiplayerNetworkManager : MonoBehaviourPunCallbacks
{
    public delegate void MultiplayerNetworkEvent();
    public delegate void MultiplayerNetworkPlayerEvent(Player player);
    public event MultiplayerNetworkEvent OnJoinLobby, OnJoinRoom, OnAttemptingToJoinRoom, OnFailedToJoinRoom, OnConnectedToServer, OnRoomPropertiesChanged;
    public event MultiplayerNetworkPlayerEvent OnPlayerJoinedRoom, OnPlayerExitRoom, OnPlayerPropertiesChanged;


    public static MultiplayerNetworkManager Instance;
    public float maxSearchTime = 5f;

    void Awake()
    {
        if (Instance == null) { Instance = this; }
        else { Destroy(this); return; }
        DontDestroyOnLoad(this);

        ConnectToMaster();
    }

    void ConnectToMaster()
    {
        PhotonNetwork.OfflineMode = false;
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.GameVersion = "0.0.1";

        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
        OnConnectedToServer?.Invoke();
        Debug.Log("Connected to Master");
    }

    public override void OnJoinedLobby()
    {
        base.OnJoinedLobby();
        OnJoinLobby?.Invoke();
        Debug.Log("Connected to Lobby");
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        OnJoinRoom?.Invoke();
        Debug.Log("Joined Room " + PhotonNetwork.CurrentRoom.Name);
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        base.OnJoinRandomFailed(returnCode, message);
        OnFailedToJoinRoom?.Invoke();
        Debug.Log(message);
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        base.OnDisconnected(cause);
        Debug.Log("Disconnected from Master");
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        base.OnRoomListUpdate(roomList);
        Debug.Log("Room list updated. Room Count " + roomList.Count);

        if (PhotonNetwork.InLobby && roomList.Count > 0)
        {
            RoomInfo info = roomList[0];
            JoinRoom(info.Name);
            StopAllCoroutines();
        }
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        base.OnPlayerEnteredRoom(newPlayer);
        OnPlayerJoinedRoom?.Invoke(newPlayer);
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        base.OnPlayerLeftRoom(otherPlayer);
        OnPlayerExitRoom?.Invoke(otherPlayer);
    }

    public override void OnPlayerPropertiesUpdate(Player target, ExitGames.Client.Photon.Hashtable changedProps)
    {
        base.OnPlayerPropertiesUpdate(target, changedProps);
        OnPlayerPropertiesChanged?.Invoke(target);
    }

    public override void OnRoomPropertiesUpdate(ExitGames.Client.Photon.Hashtable propertiesThatChanged)
    {
        base.OnRoomPropertiesUpdate(propertiesThatChanged);
        OnRoomPropertiesChanged?.Invoke();
    }

    public void BeginRoomSearch()
    {
        StartCoroutine(SearchForRoom());
    }

    public void CancelRoomSearch()
    {
        StopAllCoroutines();
    }

    private IEnumerator SearchForRoom()
    {
        yield return new WaitForSeconds(maxSearchTime);
        RoomOptions options = new RoomOptions() { MaxPlayers = GameManager.Instance.maxPlayers };
        CreateRoom(options);
    }

    public void JoinLobby()
    {
        TypedLobby typedLobby = new TypedLobby(GameManager.Instance.GetGameMode(), LobbyType.Default);

        Debug.Log("Attempting to join Lobby " + typedLobby.Name);
        PhotonNetwork.JoinLobby(typedLobby);
    }

    public void JoinRoom(string name)
    {
        OnAttemptingToJoinRoom?.Invoke();
        PhotonNetwork.JoinRoom(name);
    }

    public void CreateRoom(RoomOptions options)
    {
        Debug.Log("Creating Room");
        OnAttemptingToJoinRoom?.Invoke();
        TypedLobby typedLobby = new TypedLobby(GameManager.Instance.GetGameMode(), LobbyType.Default);
        PhotonNetwork.CreateRoom(null, options, typedLobby);
    }

    public void LeaveLobby()
    {
        if (PhotonNetwork.InLobby)
        {
            Debug.Log("LeavingLobby");
            PhotonNetwork.LeaveLobby();
        }
    }

    public void LeaveRoom()
    {
        if (PhotonNetwork.InRoom)
        {
            Debug.Log("Leaving Room");
            PhotonNetwork.LeaveRoom();
        }
    }

    public void SetNickname(string nickname)
    {
        if (PhotonNetwork.IsConnected)
        {
            PhotonNetwork.NickName = nickname;
        }
    }
}
