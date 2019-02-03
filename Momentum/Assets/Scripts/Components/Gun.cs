using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public GunProfile gunProfile;
    public LayerMask playerMask;

    private GroundDetection gd;

    private float currentSpread = 0f;
    private float spreadVelocity = 0f;
    private float remainingCooldown = 0f;
    private float remainingSpreadDecay = 0f;


    public void Start()
    {
        gd = GetComponent<GroundDetection>();
    }

    public void Update()
    {
        remainingCooldown = remainingCooldown - Time.deltaTime;
        Debug.Log(currentSpread);
    }

    public void FixedUpdate()
    {
        if (gd?.isGrounded ?? true)
            ReduceSpread(gunProfile.maxSpreadGround, gunProfile.minSpreadGround);
        else
            ReduceSpread(gunProfile.maxSpreadAir, gunProfile.minSpreadAir);

        remainingSpreadDecay = Mathf.Max(remainingSpreadDecay - Time.fixedDeltaTime, 0f);
    }

    private void ReduceSpread(float maxSpread, float minSpread)
    {
        currentSpread = Mathf.SmoothDamp(currentSpread, minSpread, ref spreadVelocity, remainingSpreadDecay);
    }

    public void Shoot()
    {
        if (remainingCooldown > 0f) { return; }
        remainingCooldown = gunProfile.fireRate;

        float minSpread, maxSpread, spreadRate;

        if (gd?.isGrounded ?? true)
        {
            minSpread = gunProfile.minSpreadGround;
            maxSpread = gunProfile.maxSpreadGround;
            spreadRate = gunProfile.spreadRateGround;
            remainingSpreadDecay = gunProfile.spreadDecayRateGround;
        }
        else
        {
            minSpread = gunProfile.minSpreadAir;
            maxSpread = gunProfile.maxSpreadAir;
            spreadRate = gunProfile.spreadRateAir;
            remainingSpreadDecay = gunProfile.spreadDecayRateAir;
        }

        
        float spreadX = Random.Range(-currentSpread, currentSpread), spreadY = Random.Range(-currentSpread, currentSpread);
        Quaternion spreadRotation = Quaternion.Euler(spreadX, 0f, spreadY);
        
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
        ray.origin = Vector3.Project(gameObject.transform.forward, ray.direction);
        ray.direction = spreadRotation * ray.direction;

        if (Physics.Raycast(ray, out hit, Camera.main.farClipPlane, playerMask, QueryTriggerInteraction.Ignore))
        {
            hit.collider.gameObject.GetComponent<IDamagable>()?.takeDamage(DamageToPlayer(hit));
        }

        BulletRenderer.RenderBullet(ray, gunProfile.falloffRange);
        currentSpread = Mathf.Clamp(currentSpread + (maxSpread - currentSpread) * spreadRate, 0f, maxSpread);
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

}
