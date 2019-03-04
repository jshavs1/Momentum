using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LaunchPage : Page
{
    public GameObject ActivityLayout, InputLayout;
    public InputField inputField;

    private void OnEnable()
    {
        ActivityLayout.SetActive(true);
        InputLayout.SetActive(false);
        MultiplayerNetworkManager.Instance.OnConnectedToServer += OnConnectedToServer;
    }

    private void OnDisable()
    {
        MultiplayerNetworkManager.Instance.OnConnectedToServer -= OnConnectedToServer;
    }

    void OnConnectedToServer()
    {
        ActivityLayout.SetActive(false);
        InputLayout.SetActive(true);
    }

    public void SubmitNickname()
    {
        string nickname = inputField.text;

        if (string.IsNullOrEmpty(nickname))
            nickname = "Bob";

        MultiplayerNetworkManager.Instance.SetNickname(nickname);
        GetComponentInParent<MenuNavigationController>()?.NavigateTo("HomePage");
    }
}
