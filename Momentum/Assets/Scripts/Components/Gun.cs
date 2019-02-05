using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Gun : MonoBehaviour
{
    public GunProfile gunProfile;
    public LayerMask playerMask;
    
    private float currentSpread = 0f;
    private float spreadVelocity = 0f;
    private float remainingCooldown = 0f;
    private float remainingSpreadDecay = 0f;

    private float minSpread, maxSpread, spreadRate, spreadDecayRate;

    public void Update()
    {
        remainingCooldown = remainingCooldown - Time.deltaTime;
    }

    public void FixedUpdate()
    {

        ReduceSpread();

        remainingSpreadDecay = Mathf.Max(remainingSpreadDecay - Time.fixedDeltaTime, 0f);
    }

    private void ReduceSpread()
    {
        currentSpread = Mathf.SmoothDamp(currentSpread, minSpread, ref spreadVelocity, remainingSpreadDecay);
    }

    public void SetState(bool grounded)
    {
        if (grounded)
        {
            minSpread = gunProfile.minSpreadGround;
            maxSpread = gunProfile.maxSpreadGround;
            spreadRate = gunProfile.spreadRateGround;
            spreadDecayRate = gunProfile.spreadDecayRateGround;
        }
        else
        {
            minSpread = gunProfile.minSpreadAir;
            maxSpread = gunProfile.maxSpreadAir;
            spreadRate = gunProfile.spreadRateAir;
            spreadDecayRate = gunProfile.spreadDecayRateAir;
        }
    }

    public void Shoot()
    {
        if (remainingCooldown > 0f) { return; }
        remainingCooldown = gunProfile.fireRate;
        remainingSpreadDecay = spreadDecayRate;


        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));

        Vector3 spreadCoordinates = Random.onUnitSphere;
        Vector3 dir = ray.direction * gunProfile.falloffRange;
        dir.x += spreadCoordinates.x * currentSpread;
        dir.y += spreadCoordinates.y * currentSpread;

        ray.direction = dir.normalized;
        ray.origin = transform.position - Vector3.Project(transform.position - ray.origin, Camera.main.transform.right) - Vector3.Project(transform.position - ray.origin, Camera.main.transform.up);

        Vector3 bulletDir = ray.direction;

        GetComponent<PhotonView>()?.RPC("RPCShoot", RpcTarget.All, ray.origin, ray.direction, bulletDir);
        currentSpread = Mathf.Clamp(currentSpread + (maxSpread - currentSpread) * spreadRate, 0f, maxSpread);
    }

    [PunRPC]
    public void RPCShoot(Vector3 origin, Vector3 dir, Vector3 bulletDir)
    {
        RaycastHit hit;
        Ray ray = new Ray(origin, dir);

        if (Physics.Raycast(ray, out hit, Camera.main.farClipPlane, playerMask, QueryTriggerInteraction.Ignore))
        {
            hit.collider.gameObject.GetComponent<IDamagable>()?.takeDamage(DamageToPlayer(hit));
            bulletDir = (hit.point - transform.position).normalized;
        }

        Debug.DrawRay(ray.origin, ray.direction * Camera.main.farClipPlane, Color.green, 0.5f);

        RenderBullet(transform.position, bulletDir, gunProfile.falloffRange);
    }

    private float DamageToPlayer(RaycastHit hit)
    {
        float damage = gunProfile.damagePerShot;
        if (hit.distance > gunProfile.falloffRange)
        {
            if (gunProfile.compoundFalloff)
            {
                damage = damage * Mathf.Pow((1f - gunProfile.falloff), Mathf.Floor(hit.distance - gunProfile.falloffRange));
            }
            else
            {
                damage = damage * (1f - gunProfile.falloff);
            }
        }
        return damage;
    }


    private void RenderBullet(Vector3 origin, Vector3 direction, float distance)
    {
        GameObject bulletTrail = Instantiate(gunProfile.bulletTrail.gameObject, origin, Quaternion.identity);
        bulletTrail.GetComponent<BulletTrail>().SetPath(origin, direction, distance);
    }
}
