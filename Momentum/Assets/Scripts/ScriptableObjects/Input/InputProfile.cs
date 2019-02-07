using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InputProfile : ScriptableObject
{
    public abstract float x();
    public abstract float y();
    public abstract float h();
    public abstract float v();
    public abstract float scroll();

    public abstract bool JumpPress();
    public abstract bool PrimaryPress();
    public abstract bool SecondaryPress();
    public abstract bool Ability1Press();
    public abstract bool Ability2Press();
    public abstract bool Ability3Press();
    public abstract bool JumpHold();
    public abstract bool PrimaryHold();
    public abstract bool SecondaryHold();
    public abstract bool Ability1Hold();
    public abstract bool Ability2Hold();
    public abstract bool Ability3Hold();
    public abstract bool DidScrollUp();
    public abstract bool DidScrollDown();
}
