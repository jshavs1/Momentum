using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct InputFrame
{
    public static InputFrame input;

    public float x;
    public float y;
    public float h;
    public float v;
    public float scroll;

    public bool JumpPress;
    public bool PrimaryPress;
    public bool SecondaryPress;
    public bool Ability3Press;
    public bool Ability4Press;
    public bool Ability5Press;
    public bool JumpHold;
    public bool PrimaryHold;
    public bool SecondaryHold;
    public bool Ability3Hold;
    public bool Ability4Hold;
    public bool Ability5Hold;
    public bool DidScrollUp;
    public bool DidScrollDown;

    public static InputFrame GetFrame()
    {
        return input;
    }

    public static void Update()
    {
        input = new InputFrame(GameManager.Instance.inputProfile);
    }

    public InputFrame(InputProfile inputProfile)
    {
        x = inputProfile.x();
        y = inputProfile.y();
        h = inputProfile.h();
        v = inputProfile.v();
        scroll = inputProfile.scroll();

        CameraController cc;
        if (cc = Camera.main.GetComponent<CameraController>())
        {
            Vector2 relInput = cc.InputToCameraSpace(new Vector2(x, y));
            x = relInput.x;
            y = relInput.y;
        }

        JumpPress = inputProfile.JumpPress();
        JumpHold = inputProfile.JumpHold();

        PrimaryPress = inputProfile.PrimaryPress();
        PrimaryHold = inputProfile.PrimaryHold();

        SecondaryPress = inputProfile.SecondaryPress();
        SecondaryHold = inputProfile.SecondaryHold();

        Ability3Press = inputProfile.Ability1Press();
        Ability3Hold = inputProfile.Ability1Hold();

        Ability4Press = inputProfile.Ability2Press();
        Ability4Hold = inputProfile.Ability2Hold();

        Ability5Press = inputProfile.Ability3Press();
        Ability5Hold = inputProfile.Ability3Hold();

        DidScrollUp = inputProfile.DidScrollUp();
        DidScrollDown = inputProfile.DidScrollDown();
    }

    public static InputFrame none
    {
        get
        {
            return new InputFrame();
        }
    }
}
