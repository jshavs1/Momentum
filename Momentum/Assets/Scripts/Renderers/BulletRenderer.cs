using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class BulletRenderer : MonoBehaviour
{
    private static BulletRenderer Instance;

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

    public static void RenderBullet(Vector3 origin, Vector3 direction, float distance)
    {
    }
}
