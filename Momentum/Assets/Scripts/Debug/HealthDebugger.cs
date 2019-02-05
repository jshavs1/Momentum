using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthDebugger : MonoBehaviour
{
    public void Start()
    {
        gameObject.GetComponent<Renderer>().material.color = Color.green;
    }

    public void OnHealthChanged(float prev, float current)
    {
        gameObject.GetComponent<Renderer>().material.color = Color.Lerp(Color.red, Color.green, current / 100f);
    }
}
