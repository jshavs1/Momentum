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
    public Text detailsText;

    private const int MATCH_COUNTDOWN_TIME = 5;
    private Coroutine matchCountdown;
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

        StopAllCoroutines();
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

        playerDict[player.ActorNumber] = player;

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
        UpdateMatchCountdown();
    }

    void OnPlayerExitRoom(Player player)
    {
        Debug.Log("Player left Room: " + player.NickName);
        playerDict.Remove(player.ActorNumber);
        blueTeam.PlayerListUpdated(playerList);
        redTeam.PlayerListUpdated(playerList);
        UpdateMatchCountdown();
    }

    void OnPlayerPropertiesUpdated(Player player)
    {
        Debug.Log("Player properties updated");

        if (player.IsLocal)
            readyButton.interactable = true;

        blueTeam.PlayerListUpdated(playerList);
        redTeam.PlayerListUpdated(playerList);

        UpdateMatchCountdown();
    }

    public void AssignPlayerToTeam(Player player, TeamList list)
    {
        if (!PhotonNetwork.IsMasterClient) { return; }

        Debug.Log("Assigning player to " + list.team + " team");

        ExitGames.Client.Photon.Hashtable playerProps = new ExitGames.Client.Photon.Hashtable { { "team", (byte)list.team }, { "ready", false } };
        player.SetCustomProperties(playerProps);
    }

    public void UpdateMatchCountdown()
    {
        if (IsReadyToStartMatch())
        {
            Debug.Log("Beginning Match countdown");
            BeginMatchCountdown();
        }
        else
        {
            Debug.Log("Match countdown cancelled");
            CancelMatchCountdown();
        }
    }

    public bool IsReadyToStartMatch()
    {
        bool b = true;
        foreach (Player player in playerDict.Values)
        {
            if ((bool)player.CustomProperties["ready"] == false)
            {
                b = false;
                break;
            }
        }
        return b;
    }

    public void BeginMatchCountdown()
    {
        if (matchCountdown == null)
            matchCountdown = StartCoroutine(MatchCountdown());
    }

    public void CancelMatchCountdown()
    {
        if (matchCountdown != null)
        {
            StopCoroutine(matchCountdown);
            matchCountdown = null;
        }
        detailsText.text = "Waiting for all players to be ready";
    }

    public void MatchCountdownFinished()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            Debug.Log("Countdown Finished");
            detailsText.text = "Starting Match";
            MultiplayerNetworkManager.Instance.BeginMatch();
        }
    }

    IEnumerator MatchCountdown()
    {
        int remainingTime = MATCH_COUNTDOWN_TIME;
        string startingMatchIn = "Starting Match in ";
        detailsText.text = startingMatchIn + remainingTime + "...";

        while (remainingTime > 0)
        {
            yield return new WaitForSecondsRealtime(1f);
            remainingTime--;
            detailsText.text = startingMatchIn + remainingTime + "...";
        }

        MatchCountdownFinished();
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
