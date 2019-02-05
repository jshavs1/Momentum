using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuButtonGroup : MonoBehaviour
{
    public Button joinRoom, createRoom, refresh;
    GameObject selectedCell;
    // Start is called before the first frame update
    void Awake()
    {
        joinRoom.interactable = false;
    }

    public void OnSelectedRoomChanged(GameObject selectedCell)
    {
        this.selectedCell = selectedCell;
        joinRoom.interactable = selectedCell != null;
    }

    public void JoinRoom()
    {
        MultiplayerNetworkManager.Instance.JoinRoom(selectedCell.GetComponent<RoomCell>().roomName);
    }

    public void CreateRoom()
    {
        MultiplayerNetworkManager.Instance.CreateRoom(null);
    }
}
