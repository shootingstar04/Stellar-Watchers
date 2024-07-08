using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    private Vector2 parallaxEffectMultiplier = new Vector2(0.4f, 0.1f);

    private Transform CameraTransform;
    private Vector3 lastCameraPosition;


    private void Start()
    {
        CameraTransform = Camera.main.transform;
        lastCameraPosition = CameraTransform.position;
    }

    private void LateUpdate()
    {
        Vector3 deltaMovement = CameraTransform.position - lastCameraPosition;

        transform.position += new Vector3(deltaMovement.x * parallaxEffectMultiplier.x, deltaMovement.y * parallaxEffectMultiplier.y, deltaMovement.z);
        lastCameraPosition= CameraTransform.position;
    }
}
