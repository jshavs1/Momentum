using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Projectile", menuName = "Ability/Projectile")]
public class ProjectileAbilityProfile : AbilityProfile
{
    public int ammo;
    public bool automatic;
    public bool firstShotDelay;
}
