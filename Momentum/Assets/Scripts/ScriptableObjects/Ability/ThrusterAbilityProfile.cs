using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Thruster Profile", menuName = "Ability/Thruster")]
public class ThrusterAbilityProfile : AbilityProfile
{
    public float thrust;
    public float maxYVel;
    public float fuel;

    protected override Type AbilityStateType { get { return typeof(ThrusterState); } }

    protected override void Initialize(AbilityState state)
    {
        ThrusterState thruster = state as ThrusterState;
        thruster.ability = this;
        thruster.remainingFuel = fuel;
    }
}
