using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    PlayerStateMachine psm;

    // Start is called before the first frame update
    void Awake()
    {
        psm = new PlayerStateMachine();
        psm.currentState = new GroundedState(psm);
    }

    // Update is called once per frame
    void Update()
    {
        psm.Update(InputFrame.GetFrame(), gameObject);
    }

    private void FixedUpdate()
    {
        psm.FixedUpdate(InputFrame.GetFrame(), gameObject);
    }
}
