using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHitSendable
{
    HitInfo GetHitInfo(float dist, Vector3 dir);
}
