using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class HitscanAbility : AbilityStateMachine, IHitSendable
{
    public HitscanInfo hitscanInfo;
    private HitscanModel model;

    private void Awake()
    {
        model = new HitscanModel();
    }

    public override void FixedUpdate()
    {
        ReduceSpread();

        model.remainingSpreadRecovery = Mathf.Max(model.remainingSpreadRecovery - Time.fixedDeltaTime, 0f);
    }

    private void ReduceSpread()
    {
        model.currentSpread = Mathf.SmoothDamp(model.currentSpread, model.minSpread, ref model.spreadVelocity, model.remainingSpreadRecovery);
    }

    public void Shoot()
    {
        if (!abilityReady) { return; }
        remainingCooldown = hitscanInfo.fireRate;
        model.remainingSpreadRecovery = hitscanInfo.spreadRecoveryRate;


        Ray ray = Camera.main.GetComponent<CameraController>().cameraRay;

        Vector3 spreadCoordinates = Random.onUnitSphere;
        Vector3 dir = ray.direction * hitscanInfo.falloffRange;
        dir += spreadCoordinates * model.currentSpread;

        ray.direction = dir.normalized;

        Vector3 bulletDir = ray.direction;

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Camera.main.farClipPlane, ~0, QueryTriggerInteraction.Ignore))
        {
            bulletDir = (hit.point - transform.position).normalized;
            hit.collider.gameObject.GetComponent<IHitReceivable>().ReceiveHit(this, hit.distance, bulletDir);
        }

        Debug.DrawRay(ray.origin, ray.direction * Camera.main.farClipPlane, Color.green, 0.5f);

        model.currentSpread = Mathf.Clamp(model.currentSpread + (hitscanInfo.maxSpread - model.currentSpread) * hitscanInfo.spreadRate, 0f, hitscanInfo.maxSpread);

        GetComponent<PhotonView>()?.RPC("RenderBulletRPC", RpcTarget.All, bulletDir, hit.distance);
    }

    [PunRPC]
    public void RenderBulletRPC(Vector3 bulletDir, float distance)
    {
        RenderBullet(transform.position, bulletDir, distance);
    }

    public HitInfo GetHitInfo(float dist, Vector3 dir)
    {
        HitInfo info = hitscanInfo.hitInfo.Clone();

        if (dist > hitscanInfo.falloffRange)
        {
            if (hitscanInfo.compoundFalloff)
            {
                float falloff = Mathf.Pow((1f - hitscanInfo.falloffAmount), Mathf.Floor(dist - hitscanInfo.falloffRange));
                info.enemyHealthDelta *= falloff;
                info.enemyKnockback *= falloff;
                info.teammateHealthDelta *= falloff;
                info.teammateKnockback *= falloff;
            }
            else
            {
                float falloff = (1f - hitscanInfo.falloffAmount);
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

    private void RenderBullet(Vector3 origin, Vector3 direction, float distance)
    {
        GameObject bulletTrail = Instantiate(hitscanInfo.bulletTrail.gameObject, origin, Quaternion.identity);
        bulletTrail.GetComponent<BulletTrail>().SetPath(origin, direction, distance);
    }
}
