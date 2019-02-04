using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPath : MonoBehaviour
{
    private LineRenderer lineRenderer;
    public float duration;
    // Start is called before the first frame update
    void Awake()
    {
        lineRenderer = gameObject.AddComponent<LineRenderer>();
    }

    public void SetPath(Vector3 origin, Vector3 direction, float distance)
    {
        transform.position = origin;
        transform.rotation = Quaternion.LookRotation(direction);

        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, origin);
        lineRenderer.SetPosition(1, origin + Vector3.ClampMagnitude(direction * Camera.main.farClipPlane, distance));
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.5f;

        StartCoroutine(DrawPath());
    }

    IEnumerator DrawPath()
    {
        float currentDuration = 0;
        while(currentDuration < duration)
        {
            currentDuration += Time.deltaTime;
            lineRenderer.startWidth = Mathf.Lerp(0.1f, 0f, currentDuration / duration);
            lineRenderer.endWidth = Mathf.Lerp(0.5f, 0f, currentDuration / duration);
            yield return null;
        }
        Destroy(gameObject);
    }
}
