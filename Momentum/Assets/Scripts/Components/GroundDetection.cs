using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundDetection : MonoBehaviour
{
    public delegate void GroundDetectionEvent();
    public event GroundDetectionEvent OnGroundEnter;
    public event GroundDetectionEvent OnGroundExit;

    private int _overlapping;
    private int overlapping
    {
        get
        {
            return _overlapping;
        }
        set
        {
            if (_overlapping <= 0 && value > 0)
                OnGroundEnter();
            if (_overlapping > 0 && value <= 0)
                OnGroundExit();

            _overlapping = value;

        }
    }
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
            overlapping++;
    }

    public void OnTriggerExit(Collider other)
    {
        if (useTrigger)
            overlapping--;
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (!useTrigger)
            overlapping++;
    }

    public void OnCollisionExit(Collision collision)
    {
        if (!useTrigger)
            overlapping--;
    }
}
