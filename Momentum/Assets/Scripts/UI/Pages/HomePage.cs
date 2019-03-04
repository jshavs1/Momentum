using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomePage : Page
{
    public void OnPlayClick()
    {
        GetComponentInParent<MenuNavigationController>()?.NavigateTo("PlayPage");
    }
}
