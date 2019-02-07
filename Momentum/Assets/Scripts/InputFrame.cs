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
    public bool Ability1Press;
    public bool Ability2Press;
    public bool Ability3Press;
    public bool JumpHold;
    public bool PrimaryHold;
    public bool SecondaryHold;
    public bool Ability1Hold;
    public bool Ability2Hold;
    public bool Ability3Hold;
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

        Ability1Press = inputProfile.Ability1Press();
        Ability1Hold = inputProfile.Ability1Hold();

        Ability2Press = inputProfile.Ability2Press();
        Ability2Hold = inputProfile.Ability2Hold();

        Ability3Press = inputProfile.Ability3Press();
        Ability3Hold = inputProfile.Ability3Hold();

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
