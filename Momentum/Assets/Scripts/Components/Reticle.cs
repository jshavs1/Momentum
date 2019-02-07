using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reticle : MonoBehaviour
{
    public Texture2D reticle;
    private GunSM gun;
    private CameraController cc;
    private float texWidth, texHeight;

    private void Start()
    {
        gun = GetComponent<GunSM>();
        cc = Camera.main.GetComponent<CameraController>();

    }

    private void Update()
    {
        float perceivedSize, actualSize = gun.currentSpread, distanceToObject = gun.currentGun.falloffRange, distanceToCam = cc.currentDistanceAway;
        perceivedSize = (actualSize / distanceToObject) * distanceToCam;

        Vector3 worldPos = cc.gameObject.transform.position;
        worldPos += cc.gameObject.transform.rotation * ((Vector3.forward * distanceToCam) - (Vector3.right * perceivedSize) - (Vector3.up * perceivedSize));
        
        Vector3 screenPos = Camera.main.WorldToScreenPoint(worldPos);

        texWidth = (int) Mathf.Abs((Screen.width / 2) - screenPos.x) * 2;
        texHeight = (int) Mathf.Abs((Screen.height / 2) - screenPos.y) * 2;
    }

    private void OnGUI()
    {
        float xMin = (Screen.width / 2) - (texWidth / 2);
        float yMin = (Screen.height / 2) - (texHeight / 2);

        GUI.DrawTexture(new Rect(xMin, yMin, texWidth, texHeight), reticle);
    }
}
