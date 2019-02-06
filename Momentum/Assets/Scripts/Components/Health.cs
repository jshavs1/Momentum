using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Photon.Pun;

public class Health : MonoBehaviour, IDamagable, IPunObservable
{
    private float _currentHitPoints;
    private float currentHitPoints
    {
        get
        {
            return _currentHitPoints;
        }
        set
        {
            float nextHitPoints = Mathf.Clamp(value, 0f, hitPoints);
            if (!Mathf.Approximately(_currentHitPoints, value))
                HealthChanged(_currentHitPoints, nextHitPoints);
            if (Mathf.Approximately(nextHitPoints, 0f))
                nextHitPoints = hitPoints;
            _currentHitPoints = nextHitPoints;
        }
    }
    public float hitPoints;

    [SerializeField]
    public HealthEvent OnHealthChanged, OnHealthZero, OnHealthFull;

    public void Start()
    {
        currentHitPoints = hitPoints;
    }

    public void takeDamage(float damage)
    {
        GetComponent<PhotonView>()?.RPC("takeDamageRPC", RpcTarget.All, damage);
    }

    [PunRPC]
    public void takeDamageRPC(float damage)
    {
        currentHitPoints -= damage;
    }

    private void HealthChanged(float prevHitPoints, float nextHitPoints)
    {
        if (Mathf.Approximately(nextHitPoints, 0f))
            OnHealthZero.Invoke(prevHitPoints, 0f);
        else if (Mathf.Approximately(nextHitPoints, hitPoints))
            OnHealthFull.Invoke(prevHitPoints, hitPoints);
        else
            OnHealthChanged.Invoke(prevHitPoints, nextHitPoints);

    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(_currentHitPoints);
        }
        else
        {
            _currentHitPoints = (float) stream.ReceiveNext();
        }
    }
}

[System.Serializable]
public class HealthEvent : UnityEvent<float, float> { }
