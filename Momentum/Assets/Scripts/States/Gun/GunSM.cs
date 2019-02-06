using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class GunSM : StateMachine, IPunObservable
{
    public GunProfile gunProfile;
    public LayerMask gunMask;
    
    public float currentSpread = 0f;
    private float spreadVelocity = 0f;
    private float remainingCooldown = 0f;
    private float remainingSpreadRecovery = 0f;

    public float minSpread, maxSpread, spreadRate, spreadRecoveryRate;

    public override State StartingState()
    {
        return new HipFireState(this);
    }

    public override void Update()
    {
        base.Update();

        remainingCooldown = remainingCooldown - Time.deltaTime;
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();

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
        remainingCooldown = gunProfile.fireRate;
        remainingSpreadRecovery = spreadRecoveryRate;


        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));

        Vector3 spreadCoordinates = Random.onUnitSphere;
        Vector3 dir = ray.direction * gunProfile.falloffRange;
        dir += spreadCoordinates * currentSpread;

        ray.direction = dir.normalized;
        ray.origin = transform.position - Vector3.Project(transform.position - ray.origin, Camera.main.transform.right) - Vector3.Project(transform.position - ray.origin, Camera.main.transform.up);

        Vector3 bulletDir = ray.direction;

        //GetComponent<PhotonView>()?.RPC("RPCShoot", RpcTarget.All, ray.origin, ray.direction, bulletDir);
        //ShootRPC(ray.origin, ray.direction, bulletDir);
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

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        throw new System.NotImplementedException();
    }
}
