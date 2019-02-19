using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

[RequireComponent(typeof(DestroyInTime))]
public class Grenade : Projectile
{
    public bool beginTimerOnCollision;
    public float explosionRadius;
    public float maxKnockback, minKnockback;
    public float maxDamage, minDamage;
    public float upwardKnockback;

    private void Start()
    {
        if (!beginTimerOnCollision)
            GetComponent<DestroyInTime>().StartTimer();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (beginTimerOnCollision)
            GetComponent<DestroyInTime>().StartTimer();
    }

    private void OnDestroy()
    {
        Explode();
    }

    private void Explode()
    {
        if (!isLocal) { return; }

        foreach (Collider c in Physics.OverlapSphere(transform.position, explosionRadius))
        {
            float dist = Vector3.Distance(transform.position, c.gameObject.transform.position);
            c.gameObject.GetComponent<IDamagable>()?.takeDamage(Mathf.Lerp(maxDamage, minDamage, dist / explosionRadius));
            c.gameObject.GetComponent<RigidbodyController>()?.TakeKnockback(Mathf.Lerp(maxKnockback, minKnockback, dist / explosionRadius), (c.gameObject.transform.position - transform.position + Vector3.up * upwardKnockback).normalized);
        }
    }
}
