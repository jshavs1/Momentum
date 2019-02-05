using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Gun Input Profile", menuName = "GunProfile", order = 1)]
public class GunProfile : ScriptableObject
{
    public BulletTrail bulletTrail;

    public float damagePerShot;
    public float knockbackPerShot;
    public float fireRate;

    public float maxSpread, minSpread;
    public float spreadRate;
    public float spreadRecoveryRate;

    public float airMaxSpread, airMinSpread;
    public float airSpreadRate;
    public float airSpreadRecoveryRate;

    [Range(0f, 1f)]
    public float ADSAccuracy;
    [Range(0f, 1f)]
    public float ADSMovementSpeed;

    public float falloffRange;
    public float falloff;

    public bool compoundFalloff;
}
