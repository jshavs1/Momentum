using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;

    public float limitX, limitY;
    public float shiftX, shiftY;
    public float angularSpeedX, angularSpeedY;
    public float distanceAway, currentDistanceAway;
    public float adsFocusTime;

    private float deltaX, deltaY, remainingFocusTime;
    private Coroutine focusRoutine;

    private void Start()
    {
        currentDistanceAway = distanceAway;
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
        Vector3 dist = new Vector3(0f, 0f, -currentDistanceAway) + Vector3.right * shiftX + Vector3.up * shiftY;
        Vector3 targetPosition = rot * dist + target.position;

        transform.rotation = rot;
        transform.position = targetPosition;
    }

    public Ray cameraRay
    {
        get
        {
            Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
            ray.origin = target.transform.position - Vector3.Project(target.transform.position - ray.origin, Camera.main.transform.right) - Vector3.Project(target.transform.position - ray.origin, Camera.main.transform.up);
            return ray;
        }
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

    public void MoveTo(float newDistance)
    {
        if (focusRoutine != null)
            StopCoroutine(focusRoutine);

        focusRoutine = StartCoroutine(MoveToRoutine(newDistance));
    }

    public void MoveBack()
    {
        if (focusRoutine != null)
            StopCoroutine(focusRoutine);

        focusRoutine = StartCoroutine(MoveBackRoutine());
    }

    public IEnumerator MoveToRoutine(float newDistance)
    {
        float time = 0f, prevDistance = currentDistanceAway;

        while(time < adsFocusTime)
        {
            currentDistanceAway = Mathf.SmoothStep(currentDistanceAway, newDistance, Mathf.Clamp01(time / adsFocusTime));
            yield return null;
            time += Time.deltaTime;
        }

        currentDistanceAway = newDistance;
    }

    public IEnumerator MoveBackRoutine()
    {
        float time = 0f;
        float prevDistance = currentDistanceAway;

        while (time < adsFocusTime)
        {
            currentDistanceAway = Mathf.SmoothStep(prevDistance, distanceAway, Mathf.Clamp01(time / adsFocusTime));
            yield return null;
            time += Time.deltaTime;
        }

        currentDistanceAway = distanceAway;
    }
}
