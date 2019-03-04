using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class TeamList : MonoBehaviour
{
    public Team team;
    PlayerRow[] playerRows;

    private void OnEnable()
    {
        playerRows = GetComponentsInChildren<PlayerRow>();
    }

    public void PlayerListUpdated(Player[] playerList)
    {
        Debug.Log("Updating team list");

        foreach (PlayerRow row in playerRows)
        {
            row.SetNickname(string.Empty);
        }

        foreach (Player player in playerList)
        {
            if ((Team) player.CustomProperties["team"] == team)
            {
                FindOpenRow()?.SetNickname(player.NickName);
            }
        }
    }

    public PlayerRow FindOpenRow()
    {
        foreach(PlayerRow row in playerRows)
        {
            if (row.isOpen)
                return row;
        }
        return null;
    }

    public int Count
    {
        get
        {
            int count = 0;
            foreach(PlayerRow row in playerRows)
            {
                if (!row.isOpen) { count++; }
            }
            return count;
        }
    }
}

public enum Team: byte
{
    Blue,
    Red,
    None
}
