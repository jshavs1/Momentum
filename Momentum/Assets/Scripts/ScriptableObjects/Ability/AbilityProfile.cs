using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbilityProfile : ScriptableObject
{
    public float cooldown;
    public float duration;
    public bool beginCoolDownOnCompletion, disableMovement, disablePrimary, disableSecondary, disableAbility3, disableAbility4;
}
