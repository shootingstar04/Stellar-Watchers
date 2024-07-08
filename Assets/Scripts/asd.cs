using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Unity.VisualScripting;

public class asd : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera cb;

    public static asd Instance;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Debug.Log("Instance¼³Á¤x");
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        cb = GetComponent<CinemachineVirtualCamera>();
        if (cb == null)
        {
            Debug.Log("CinemachineVirtualCamera component not found on the game object.");
        }
    }

    public void ChangeCameraTarget(Transform t)
    {
        if (cb == null)
        {
            Debug.Log("CinemachineVirtualCamera is null. Cannot change camera target.");
            return;
        }
        cb.Follow = t;
        cb.LookAt = t;
    }
}

