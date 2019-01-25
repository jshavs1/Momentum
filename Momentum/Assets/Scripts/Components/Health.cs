using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour, IDamagable
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
            if (_currentHitPoints != value)
                HealthChanged(_currentHitPoints, nextHitPoints);
            _currentHitPoints = nextHitPoints;
        }
    }
    public float hitPoints;

    public UnityEvent OnHealthChanged, OnHealthZero, OnHealthFull;

    public void takeDamage(float damage)
    {
        hitPoints -= damage;
    }

    private void HealthChanged(float prevHitPoints, float nextHitPoints)
    {
        if (nextHitPoints == 0f)
            OnHealthZero.Invoke();
        else if (nextHitPoints == hitPoints)
            OnHealthFull.Invoke();
        else
            OnHealthChanged.Invoke();

    }


}
