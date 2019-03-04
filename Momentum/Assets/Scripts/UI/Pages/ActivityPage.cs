using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ActivityPage : Page
{
    public Text infoLabel;

    private void OnEnable()
    {
        MultiplayerNetworkManager.Instance.OnJoinLobby += OnJoinLobby;
        MultiplayerNetworkManager.Instance.OnJoinRoom += OnJoinRoom;
        MultiplayerNetworkManager.Instance.OnAttemptingToJoinRoom += OnAttemptingToJoinRoom;
        MultiplayerNetworkManager.Instance.OnFailedToJoinRoom += OnFailedToJoinRoom;
    }

    private void OnDisable()
    {
        MultiplayerNetworkManager.Instance.OnJoinLobby -= OnJoinLobby;
        MultiplayerNetworkManager.Instance.OnJoinRoom -= OnJoinRoom;
        MultiplayerNetworkManager.Instance.OnAttemptingToJoinRoom -= OnAttemptingToJoinRoom;
        MultiplayerNetworkManager.Instance.OnFailedToJoinRoom -= OnFailedToJoinRoom;
    }

    protected override void OnPageEnter()
    {
        base.OnPageEnter();
        string gameMode = GameManager.Instance.GetGameMode();
        infoLabel.text = "Joining " + gameMode[0].ToString().ToUpper() + gameMode.Substring(1) + " Lobby";

        MultiplayerNetworkManager.Instance.JoinLobby();
    }

    protected override void OnPageExit()
    {
        base.OnPageExit();
    }

    private void OnJoinLobby()
    {
        MultiplayerNetworkManager.Instance.BeginRoomSearch();
        infoLabel.text = "Searching for Game";
    }

    private void OnJoinRoom()
    {
        GetComponentInParent<MenuNavigationController>().ExitCurrentModal();
        GetComponentInParent<MenuNavigationController>().NavigateTo("RoomPage");
        infoLabel.text = "Game Joined";
    }

    private void OnAttemptingToJoinRoom()
    {
        interactable = false;
        infoLabel.text = "Attempting to Join Game";
    }

    private void OnFailedToJoinRoom()
    {
        interactable = true;
        infoLabel.text = "Failed to Join Game";
    }

    public void OnCancelClick()
    {
        MultiplayerNetworkManager.Instance.CancelRoomSearch();
        MultiplayerNetworkManager.Instance.LeaveLobby();
        GetComponentInParent<MenuNavigationController>().ExitCurrentModal();
    }
}
