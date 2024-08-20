using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ImpulseManger : MonoBehaviour
{
    public static ImpulseManger Instance;

    [SerializeField] private float globalShakeForce = 1f;

    private void Awake()
    {
        if ( Instance == null)
        {
            Instance = this;
        }
    }

    public void CameraShake(CinemachineImpulseSource impulseSource)
    {
        impulseSource.GenerateImpulseWithForce(globalShakeForce);   
    }
}
