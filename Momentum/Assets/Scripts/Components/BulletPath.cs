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

    public void SetPath(Ray ray, float distance)
    {
        transform.position = ray.origin;
        transform.rotation = Quaternion.LookRotation(ray.direction);

        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, ray.origin);
        lineRenderer.SetPosition(1, ray.origin + ray.direction.normalized * distance);
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
            lineRenderer.endWidth = Mathf.Lerp(0.5f, 0f, currentDuration / duration);
            yield return null;
        }
        Destroy(gameObject);
    }
}
