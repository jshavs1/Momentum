using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ProjectileService : MonoBehaviour
{
    private static ProjectileService Instance;
    private PhotonView photonView;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            photonView = GetComponent<PhotonView>();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void InstantiateProjectile(ProjectileAbilityProfile projectileAbility)
    {

    }

    [PunRPC]
    private void InstantiateProjectileRPC(byte slot, Vector3 pos, byte[] playerInfoBin)
    {
        PlayerInfoData playerInfo = PlayerInfoData.Deserialize(playerInfoBin);
        GameObject player = PhotonView.Find(playerInfo.id).gameObject;
    }

    private AbilityStateMachine GetAbility(AbilitySlot slot, GameObject player)
    {
        foreach (AbilityStateMachine sm in player.GetComponents<AbilityStateMachine>())
        {
            if (sm.abilitySlot == slot)
                return sm;
        }
        return null;
    }
}
