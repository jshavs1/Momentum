using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameModeButton : CustomButton
{

    public override void OnHoverEnter()
    {
        base.OnHoverEnter();
        anim.SetTrigger("OnHoverEnter");
    }

    public override void OnHoverExit()
    {
        base.OnHoverExit();
        anim.SetTrigger("OnHoverExit");
    }
}
