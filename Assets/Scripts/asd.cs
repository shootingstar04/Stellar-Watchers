using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Unity.VisualScripting;

public class asd : MonoBehaviour
{
    private CinemachineVirtualCamera cb;

    public static asd Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        cb = GetComponent<CinemachineVirtualCamera>();
    }

    public void ChangeCameraTarget(Transform t)
    {
        cb.Follow = t;
        cb.LookAt = t;
    }
}
