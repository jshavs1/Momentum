using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayPage : Page
{
    private void OnGameModeClick()
    {
        GetComponentInParent<MenuNavigationController>()?.PresentAsModal("ActivityPage");
    }

    public void OnSkirmishClick()
    {
        GameManager.Instance.SetGameMode("skirmish");
        OnGameModeClick();
    }

    public void OnChaseClick()
    {
        GameManager.Instance.SetGameMode("chase");
        OnGameModeClick();
    }

    public void OnCaptureClick()
    {
        GameManager.Instance.SetGameMode("capture");
        OnGameModeClick();
    }
}
