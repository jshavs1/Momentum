using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    PhotonView PV;
    PlayerStateMachine psm;
    InputFrame currentInput;

    private void Start()
    {
        PV = GetComponent<PhotonView>();

        if (!PV.IsMine) { return; }

        psm = new PlayerStateMachine();
    }

    // Update is called once per frame
    void Update()
    {
        if (!PV.IsMine) { return; }

        currentInput = InputFrame.GetFrame();
        psm.Update(currentInput, gameObject);
    }

    private void FixedUpdate()
    {
        if (!PV.IsMine) { return; }

        psm.FixedUpdate(currentInput, gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!PV.IsMine) { return; }

        psm.NextState(new GroundedState(psm), currentInput, gameObject);
    }
}
