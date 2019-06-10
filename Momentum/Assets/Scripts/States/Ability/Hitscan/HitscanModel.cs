using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitscanModel
{
    public float currentSpread;
    public float spreadVelocity;
    public float remainingCooldown;
    public float remainingSpreadRecovery;

    public float minSpread, maxSpread, spreadRate, spreadRecoveryRate;
}
