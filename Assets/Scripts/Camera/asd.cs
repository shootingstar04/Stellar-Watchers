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
        DontDestroyOnLoad(gameObject);
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
