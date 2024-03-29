﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Gun : MonoBehaviour, IHitSendable
{
    public GunInfo gunInfo;

    public float currentSpread;
    private float spreadVelocity;
    private float remainingCooldown;
    private float remainingSpreadRecovery;

    public float minSpread, maxSpread, spreadRate, spreadRecoveryRate;

    public void Update()
    {
        remainingCooldown = remainingCooldown - Time.deltaTime;
    }

    public void FixedUpdate()
    {
        ReduceSpread();

        remainingSpreadRecovery = Mathf.Max(remainingSpreadRecovery - Time.fixedDeltaTime, 0f);
    }

    private void ReduceSpread()
    {
        currentSpread = Mathf.SmoothDamp(currentSpread, minSpread, ref spreadVelocity, remainingSpreadRecovery);
    }

    public void Shoot()
    {
        if (remainingCooldown > 0f) { return; }
        remainingCooldown = gunInfo.fireRate;
        remainingSpreadRecovery = spreadRecoveryRate;


        Ray ray = Camera.main.GetComponent<CameraController>().cameraRay;

        Vector3 spreadCoordinates = Random.onUnitSphere;
        Vector3 dir = ray.direction * gunInfo.falloffRange;
        dir += spreadCoordinates * currentSpread;

        ray.direction = dir.normalized;

        Vector3 bulletDir = ray.direction;

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Camera.main.farClipPlane, ~0, QueryTriggerInteraction.Ignore))
        {
            bulletDir = (hit.point - transform.position).normalized;
            hit.collider.gameObject.GetComponent<IHitReceivable>().ReceiveHit(GetHitInfo(hit.distance, bulletDir));
        }

        Debug.DrawRay(ray.origin, ray.direction * Camera.main.farClipPlane, Color.green, 0.5f);

        currentSpread = Mathf.Clamp(currentSpread + (maxSpread - currentSpread) * spreadRate, 0f, maxSpread);

        GetComponent<PhotonView>()?.RPC("RenderBulletRPC", RpcTarget.All, bulletDir, hit.distance);
    }

    [PunRPC]
    public void RenderBulletRPC(Vector3 bulletDir, float distance)
    {
        RenderBullet(transform.position, bulletDir, distance);
    }

    public HitInfo GetHitInfo(float dist, Vector3 dir)
    {
        HitInfo info = gunInfo.hitInfo.Clone();

        if (dist > gunInfo.falloffRange)
        {
            if (gunInfo.compoundFalloff)
            {
                float falloff = Mathf.Pow((1f - gunInfo.falloffAmount), Mathf.Floor(dist - gunInfo.falloffRange));
                info.enemyHealthDelta *= falloff;
                info.enemyKnockback *= falloff;
                info.teammateHealthDelta *= falloff;
                info.teammateKnockback *= falloff;
            }
            else
            {
                float falloff = (1f - gunInfo.falloffAmount);
                info.enemyHealthDelta *= falloff;
                info.enemyKnockback *= falloff;
                info.teammateHealthDelta *= falloff;
                info.teammateKnockback *= falloff;
            }
        }

        info.SetId(GetComponent<PhotonView>().ViewID);
        info.SetTeam((Team)PhotonNetwork.LocalPlayer.CustomProperties["team"]);
        info.SetDirection(dir);

        return info;
    }

    public void ResetSpread()
    {
        spreadVelocity = 0f;
        remainingSpreadRecovery = 0f;
        remainingCooldown = 0f;
    }


    private void RenderBullet(Vector3 origin, Vector3 direction, float distance)
    {
        GameObject bulletTrail = Instantiate(currentGun.bulletTrail.gameObject, origin, Quaternion.identity);
        bulletTrail.GetComponent<BulletTrail>().SetPath(origin, direction, distance);
    }
}
