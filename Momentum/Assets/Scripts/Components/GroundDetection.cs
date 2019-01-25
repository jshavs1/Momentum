using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundDetection : MonoBehaviour
{
    private int _overlapping = 0;
    public bool useTrigger = true;

    public bool isGrounded
    {
        get
        {
            return (_overlapping > 0);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (useTrigger)
            _overlapping++;
    }

    public void OnTriggerExit(Collider other)
    {
        if (useTrigger)
            _overlapping--;
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (!useTrigger)
            _overlapping++;
    }

    public void OnCollisionExit(Collision collision)
    {
        if (!useTrigger)
            _overlapping--;
    }
}
