using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Unity.VisualScripting;

public class asd : MonoBehaviour
{
<<<<<<< HEAD
    private CinemachineVirtualCamera cb;
=======
    [SerializeField] private CinemachineVirtualCamera cb;
>>>>>>> af53cb1c83a19e62ef889f90a2f9eb2fb512ccdd

    public static asd Instance;

    private void Awake()
    {
<<<<<<< HEAD
        Instance = this;
=======
        if (Instance != null && Instance != this)
        {
            Debug.Log("Instance¼³Á¤x");
        }
        else
        {
            Instance = this;
        }
>>>>>>> af53cb1c83a19e62ef889f90a2f9eb2fb512ccdd
    }

    private void Start()
    {
        cb = GetComponent<CinemachineVirtualCamera>();
<<<<<<< HEAD
=======
        if (cb == null)
        {
            Debug.Log("CinemachineVirtualCamera component not found on the game object.");
        }
>>>>>>> af53cb1c83a19e62ef889f90a2f9eb2fb512ccdd
    }

    public void ChangeCameraTarget(Transform t)
    {
<<<<<<< HEAD
=======
        if (cb == null)
        {
            Debug.Log("CinemachineVirtualCamera is null. Cannot change camera target.");
            return;
        }
>>>>>>> af53cb1c83a19e62ef889f90a2f9eb2fb512ccdd
        cb.Follow = t;
        cb.LookAt = t;
    }
}
<<<<<<< HEAD
=======

>>>>>>> af53cb1c83a19e62ef889f90a2f9eb2fb512ccdd
