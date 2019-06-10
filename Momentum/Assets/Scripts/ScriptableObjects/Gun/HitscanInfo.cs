using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Hitscan Info", menuName = "Abilities/Info/HitscanInfo")]
public class HitscanInfo : ScriptableObject
{
    public BulletTrail bulletTrail;

    public HitInfo hitInfo;

    public float fireRate;

    public float maxSpread, minSpread;
    public float spreadRate;
    public float spreadRecoveryRate;

    public float airMaxSpread, airMinSpread;
    public float airSpreadRate;
    public float airSpreadRecoveryRate;

    public float falloffRange;
    public float falloffAmount;

    public bool compoundFalloff;
}
