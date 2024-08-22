using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Unity.VisualScripting;
using System.ComponentModel;

public class VirtualCameraManager : MonoBehaviour
{


    [SerializeField] private CinemachineVirtualCamera cb;

    public static VirtualCameraManager Instance;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        

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

