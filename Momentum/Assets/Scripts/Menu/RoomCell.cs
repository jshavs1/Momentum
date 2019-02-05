using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomCell : MonoBehaviour
{
    private string _roomName;
    public string roomName
    {
        get
        {
            return _roomName;
        }
        set
        {
            _roomName = value;
            GetComponentInChildren<Text>().text = value;
        }
    }

    public void OnClick()
    {
        GetComponentInParent<RoomList>().CellClicked(gameObject);
    }
}
