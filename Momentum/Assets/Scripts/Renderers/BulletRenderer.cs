using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletRenderer : MonoBehaviour
{
    private static BulletRenderer Instance;
    public BulletPath bulletPathObject;

    // Start is called before the first frame update
    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
            return;
        }
    }

    public static void RenderBullet(Ray ray, float distance)
    {
        GameObject bulletPath = Instantiate(Instance.bulletPathObject.gameObject, ray.origin, Quaternion.identity);
        bulletPath.GetComponent<BulletPath>().SetPath(ray, distance);
    }
}
