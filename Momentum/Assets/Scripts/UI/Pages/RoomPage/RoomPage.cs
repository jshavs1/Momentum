using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class RoomPage : Page
{
    public TeamList blueTeam;
    public TeamList redTeam;
    public CustomButton readyButton;

    private Dictionary<int, Player> playerDict;
    private Player[] playerList
    {
        get
        {
            Player[] values = new Player[playerDict.Count];
            playerDict.Values.CopyTo(values, 0);
            return values;
        }
    }

    private void OnEnable()
    {
        playerDict = new Dictionary<int, Player>();
        foreach (Player p in PhotonNetwork.PlayerList)
        {
            playerDict.Add(p.ActorNumber, p);
        }

        if (PhotonNetwork.IsMasterClient)
        {
            AssignPlayerToTeam(PhotonNetwork.LocalPlayer, blueTeam);
        }

        MultiplayerNetworkManager.Instance.OnPlayerJoinedRoom += OnPlayerJoinedRoom;
        MultiplayerNetworkManager.Instance.OnPlayerExitRoom += OnPlayerExitRoom;
        MultiplayerNetworkManager.Instance.OnPlayerPropertiesChanged += OnPlayerPropertiesUpdated;

    }

    private void OnDisable()
    {
        MultiplayerNetworkManager.Instance.OnPlayerJoinedRoom -= OnPlayerJoinedRoom;
        MultiplayerNetworkManager.Instance.OnPlayerExitRoom -= OnPlayerExitRoom;
        MultiplayerNetworkManager.Instance.OnPlayerPropertiesChanged -= OnPlayerPropertiesUpdated;
    }

    public void OnBackClick()
    {
        UpdateReadyButton(false);

        MultiplayerNetworkManager.Instance.LeaveRoom();
        MultiplayerNetworkManager.Instance.LeaveLobby();
        GetComponentInParent<MenuNavigationController>().BackPage();
    }

    void OnPlayerJoinedRoom(Player player)
    {
        Debug.Log("Player entered Room: " + player.NickName);

        if (!PhotonNetwork.IsMasterClient) { return; }

        int blueCount = blueTeam.Count, redCount = redTeam.Count;

        if (blueCount <= redCount)
        {
            AssignPlayerToTeam(player, blueTeam);
        }
        else
        {
            AssignPlayerToTeam(player, redTeam);
        }
    }

    void OnPlayerExitRoom(Player player)
    {
        Debug.Log("Player left Room: " + player.NickName);
        playerDict.Remove(player.ActorNumber);
        blueTeam.PlayerListUpdated(playerList);
        redTeam.PlayerListUpdated(playerList);
    }

    void OnPlayerPropertiesUpdated(Player player)
    {
        Debug.Log("Player properties updated");
        playerDict[player.ActorNumber] = player;

        if (player.IsLocal)
            readyButton.interactable = true;

        blueTeam.PlayerListUpdated(playerList);
        redTeam.PlayerListUpdated(playerList);
    }

    public void AssignPlayerToTeam(Player player, TeamList list)
    {
        if (!PhotonNetwork.IsMasterClient) { return; }

        Debug.Log("Assigning player to " + list.team + " team");

        ExitGames.Client.Photon.Hashtable playerProps = new ExitGames.Client.Photon.Hashtable { { "team", (byte)list.team }, { "ready", false } };
        player.SetCustomProperties(playerProps);
    }

    public void OnReadyUpClick()
    {
        bool isReady = (bool) PhotonNetwork.LocalPlayer.CustomProperties["ready"];

        ExitGames.Client.Photon.Hashtable playerProps = new ExitGames.Client.Photon.Hashtable { { "ready", !isReady } };
        PhotonNetwork.LocalPlayer.SetCustomProperties(playerProps);

        readyButton.interactable = false;

        UpdateReadyButton(!isReady);
    }

    private void UpdateReadyButton(bool isReady)
    {
        Text text = readyButton.GetComponentInChildren<Text>();
        if (isReady)
        {
            text.color = ColorDefaults.Red;
            text.text = "Unready";
        }
        else
        {
            text.color = ColorDefaults.Ready;
            text.text = "Ready Up";
        }
    }
}
