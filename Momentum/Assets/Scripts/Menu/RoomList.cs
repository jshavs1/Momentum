using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using Photon.Realtime;

public class RoomList : MonoBehaviour
{
    [SerializeField]
    public RoomListEvent OnSelectedRoomChanged;
    public GameObject roomCell;
    GameObject selectedCell;
    List<GameObject> roomCells = new List<GameObject>();


    public void Refresh()
    {
        selectedCell = null;
        OnSelectedRoomChanged.Invoke(selectedCell);

        foreach(GameObject obj in roomCells)
        {
            Destroy(obj);
        }

        roomCells.Clear();
        foreach (RoomInfo roomInfo in MultiplayerNetworkManager.Instance.roomList)
        {
            Debug.Log(roomInfo.Name);
            GameObject roomCell = Instantiate(this.roomCell, transform);
            roomCell.GetComponent<RoomCell>().roomName = roomInfo.Name;
            roomCells.Add(roomCell);
        }
    }

    public void CellClicked(GameObject cell)
    {
        selectedCell?.GetComponentInChildren<Button>().Select();
        selectedCell = cell;

        Button b = cell.GetComponentInChildren<Button>();
        b.Select();

        OnSelectedRoomChanged.Invoke(selectedCell);
    }
}

[System.Serializable]
public class RoomListEvent : UnityEvent<GameObject> { }
