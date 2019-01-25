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


    public void Start()
    {
        gd = GetComponent<GroundDetection>();
    }

    public void Update()
    {
        remainingCooldown = remainingCooldown - Time.deltaTime;
    }

    public void FixedUpdate()
    {
        if (gd?.isGrounded ?? true)
            ReduceSpread(gunProfile.maxSpreadGround, gunProfile.minSpreadGround, gunProfile.spreadDecayRateGround);
        else
            ReduceSpread(gunProfile.maxSpreadAir, gunProfile.minSpreadAir, gunProfile.spreadDecayRateAir);
    }

    private void ReduceSpread(float maxSpread, float minSpread, float spreadDecayRate)
    {
        currentSpread = Mathf.SmoothDamp(currentSpread, minSpread, ref spreadVelocity, spreadDecayRate);
    }

    public void Shoot()
    {
        if (remainingCooldown > 0f) { return; }
        remainingCooldown = 1f / gunProfile.fireRate;

        float minSpread, maxSpread, spreadRate;

        if (gd?.isGrounded ?? true)
        {
            minSpread = gunProfile.minSpreadGround;
            maxSpread = gunProfile.maxSpreadGround;
            spreadRate = gunProfile.spreadRateGround;
        }
        else
        {
            minSpread = gunProfile.minSpreadAir;
            maxSpread = gunProfile.maxSpreadAir;
            spreadRate = gunProfile.spreadRateAir;
        }


        float spreadX = Random.Range(minSpread, currentSpread), spreadY = Random.Range(minSpread, currentSpread);
        Quaternion spreadRotation = Quaternion.Euler(spreadX, 0f, spreadY);

        Vector3 direction = Camera.main.transform.rotation * spreadRotation * Vector3.forward;

        currentSpread = Mathf.Clamp(currentSpread + (maxSpread - currentSpread) * spreadRate, 0f, maxSpread);

        RaycastHit hit;
        Ray ray = new Ray(Camera.main.transform.position, direction);

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, playerMask, QueryTriggerInteraction.Ignore))
        {
            hit.collider.gameObject.GetComponent<IDamagable>()?.takeDamage(DamageToPlayer(hit));
        }
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
