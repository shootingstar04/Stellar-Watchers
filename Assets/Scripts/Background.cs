using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    [SerializeField] private Vector2 parallaxEffectMultiplier;

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
