using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHitReceivable
{
    void ReceiveHit(IHitSendable hitSendable, float dist, Vector3 dir);
}
