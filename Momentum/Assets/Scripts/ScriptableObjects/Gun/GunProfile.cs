using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Gun Input Profile", menuName = "GunProfile", order = 1)]
public class GunProfile : ScriptableObject
{
    public float damagePerShot;
    public float knockbackPerShot;
    public float fireRate;

    public float maxSpreadGround, minSpreadGround;
    public float spreadRateGround;
    public float spreadDecayRateGround;

    public float maxSpreadAir, minSpreadAir;
    public float spreadRateAir;
    public float spreadDecayRateAir;

    public float falloffRange;
    public float falloff;

    public bool compoundFalloff;
}
