using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Keyboard Input Profile", menuName = "InputProfile/KeyboardInputProfile", order = 1)]
public class KeyboardInputProfile : InputProfile
{

    public KeyCode Left;
    public KeyCode Right;
    public KeyCode Up;
    public KeyCode Down;
    public KeyCode Jump;
    public KeyCode Primary;
    public KeyCode Secondary;
    public KeyCode Ability1;
    public KeyCode Ability2;
    public KeyCode Ability3;

    public override float x()
    {
        return (Input.GetKey(Left) ? -1f : 0f) + (Input.GetKey(Right) ? 1f : 0f);
    }
    public override float y()
    {
        return (Input.GetKey(Down) ? -1f : 0f) + (Input.GetKey(Up) ? 1f : 0f);
    }
    public override float h()
    {
        return Input.GetAxis("Mouse X");
    }
    public override float v()
    {
        return Input.GetAxis("Mouse Y");
    }
    public override float scroll()
    {
        return Input.GetAxis("Mouse ScrollWheel");
    }
    public override bool JumpPress()
    {
        return Input.GetKeyDown(Jump);
    }
    public override bool PrimaryPress()
    {
        return Input.GetKeyDown(Primary);
    }
    public override bool SecondaryPress()
    {
        return Input.GetKeyDown(Secondary);
    }
    public override bool Ability1Press()
    {
        return Input.GetKeyDown(Ability1);
    }
    public override bool Ability2Press()
    {
        return Input.GetKeyDown(Ability2);
    }
    public override bool Ability3Press()
    {
        return Input.GetKeyDown(Ability3);
    }
    public override bool JumpHold()
    {
        return Input.GetKey(Jump);
    }
    public override bool PrimaryHold()
    {
        return Input.GetKey(Primary);
    }
    public override bool SecondaryHold()
    {
        return Input.GetKey(Secondary);
    }
    public override bool Ability1Hold()
    {
        return Input.GetKey(Ability1);
    }
    public override bool Ability2Hold()
    {
        return Input.GetKey(Ability2);
    }
    public override bool Ability3Hold()
    {
        return Input.GetKey(Ability3);
    }
    public override bool DidScrollUp()
    {
        return scroll() > 0.2f;
    }
    public override bool DidScrollDown()
    {
        return scroll() < -0.2f;
    }
}

