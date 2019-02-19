using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class ProjectileTrigger : MonoBehaviour
{
    public void ShootProjectile(AbilitySlot slot)
    {
        ProjectileAbilityProfile ability = GetAbility(slot) as ProjectileAbilityProfile;
        Ray ray = Camera.main.GetComponent<CameraController>().cameraRay;

        Vector3 vel, target = ray.origin + ray.direction * ability.distance, pos = transform.position;

        ability.SolveBallisticArc(pos, target, out vel);

        GameObject projectile = InstatiateProjectile(ability, vel, pos);
        projectile.GetComponent<Projectile>().isLocal = true;

        GetComponent<PhotonView>()?.RPC("ShootProjectileRPC", RpcTarget.Others, (byte)slot, vel, pos);
    }

    [PunRPC]
    private void ShootProjectileRPC(byte slot, Vector3 vel, Vector3 pos)
    {
        ProjectileAbilityProfile ability = GetAbility((AbilitySlot)slot) as ProjectileAbilityProfile;
        GameObject projectile = InstatiateProjectile(ability, vel, pos);
    }

    private GameObject InstatiateProjectile(ProjectileAbilityProfile ability, Vector3 vel, Vector3 pos) 
    {
        GameObject projectile = Instantiate(ability.projectilePrefab, pos, Quaternion.identity) as GameObject;
        projectile.GetComponent<Projectile>().Launch(ability, vel);
        return projectile;
    }

    private AbilityProfile GetAbility(AbilitySlot slot)
    {
        foreach (AbilityStateMachine sm in GetComponents<AbilityStateMachine>())
        {
            if (sm.abilitySlot == slot)
                return sm.ability;
        }
        return null;
    }
}
