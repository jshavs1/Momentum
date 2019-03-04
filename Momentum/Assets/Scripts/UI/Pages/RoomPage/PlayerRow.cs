using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerRow : MonoBehaviour
{
    public GameObject activityIndicator;
    public Text nicknameText;
    public bool isOpen = true;

    private void OnEnable()
    {
        SetNickname(string.Empty);
    }

    public void SetNickname(string nickname)
    {
        if (!string.IsNullOrEmpty(nickname))
            isOpen = false;
        else
            isOpen = true;

        nicknameText.gameObject.SetActive(!string.IsNullOrEmpty(nickname));
        nicknameText.text = nickname;
        UpdateActivity();
    }

    public void UpdateActivity()
    {
        if (string.IsNullOrEmpty(nicknameText.text))
        {
            activityIndicator.SetActive(true);
        }
        else
        {
            activityIndicator.SetActive(false);
        }
    }
}
