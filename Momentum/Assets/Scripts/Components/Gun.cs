using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Gun : MonoBehaviour
{
    public GunProfile primaryGun, secondaryGun;
    internal GunProfile currentGun;
    internal int gunIndex;
    public LayerMask gunMask;

    public float currentSpread;
    private float spreadVelocity;
    private float remainingCooldown;
    private float remainingSpreadRecovery;

    public float minSpread, maxSpread, spreadRate, spreadRecoveryRate;

    public void Start()
    {
        currentGun = primaryGun;
    }

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
        remainingCooldown = currentGun.fireRate;
        remainingSpreadRecovery = spreadRecoveryRate;


        Ray ray = Camera.main.GetComponent<CameraController>().cameraRay;

        Vector3 spreadCoordinates = Random.onUnitSphere;
        Vector3 dir = ray.direction * currentGun.falloffRange;
        dir += spreadCoordinates * currentSpread;

        ray.direction = dir.normalized;

        Vector3 bulletDir = ray.direction;

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Camera.main.farClipPlane, gunMask, QueryTriggerInteraction.Ignore))
        {
            hit.collider.gameObject.GetComponent<IDamagable>()?.takeDamage(DamageToPlayer(hit));
            bulletDir = (hit.point - transform.position).normalized;
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

    private float DamageToPlayer(RaycastHit hit)
    {
        float damage = currentGun.damagePerShot;
        if (hit.distance > currentGun.falloffRange)
        {
            if (currentGun.compoundFalloff)
            {
                damage = damage * Mathf.Pow((1f - currentGun.falloff), Mathf.Floor(hit.distance - currentGun.falloffRange));
            }
            else
            {
                damage = damage * (1f - currentGun.falloff);
            }
        }
        return damage;
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
