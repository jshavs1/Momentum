using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;

    public float limitX, limitY;
    public float shiftX, shiftY;
    public float angularSpeedX, angularSpeedY;
    public float distanceAway;

    private float deltaX, deltaY;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Follow();
    }

    void Follow()
    {
        if (target == null) { return; }
        InputFrame input = InputFrame.GetFrame();
        deltaX += input.h * angularSpeedX * Time.deltaTime;
        deltaY += input.v * angularSpeedY * Time.deltaTime;

        deltaX = Mathf.Clamp(ClampAngle(deltaX), -limitX, limitX);
        deltaY = Mathf.Clamp(ClampAngle(deltaY), -limitY, limitY);

        Quaternion rot = Quaternion.Euler(new Vector3(-deltaY, deltaX, 0f));
        Vector3 dist = new Vector3(0f, 0f, -distanceAway) + Vector3.right * shiftX + Vector3.up * shiftY;
        Vector3 targetPosition = rot * dist + target.position;

        transform.rotation = rot;
        transform.position = targetPosition;
    }

    float ClampAngle(float angle)
    {
        if (angle > 360f)
        {
            return angle - 360f;
        }
        if (angle < -360f)
        {
            return angle + 360f;
        }
        else return angle;
    }

    public Vector2 InputToCameraSpace(Vector2 i)
    {
        Vector2 camRight = new Vector2(transform.right.x, transform.right.z);
        Vector2 camForward = new Vector2(transform.forward.x, transform.forward.z);

        camRight.Normalize();
        camForward.Normalize();

        return i.x * camRight + i.y * camForward; 
    }
}
