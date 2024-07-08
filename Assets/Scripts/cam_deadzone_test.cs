using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class cam_deadzone_test : MonoBehaviour
{
    private CinemachineVirtualCamera cam;

    private void Start()
    {
        cam = GetComponent<CinemachineVirtualCamera>();
    }

    private void Update()
    {
        float moveInput = Input.GetAxis("Horizontal");
        bool jumpInput = Input.GetKey(KeyCode.Z);

        if (moveInput != 0)
        {
            moveInput = moveInput > 0 ? 1f : -1f;
        }

        if (moveInput == 1f)
            cam.GetCinemachineComponent<CinemachineFramingTransposer>().m_ScreenX = 0.3f;
        else if(moveInput == -1f)
            cam.GetCinemachineComponent<CinemachineFramingTransposer>().m_ScreenX = 0.7f;
        else
            cam.GetCinemachineComponent<CinemachineFramingTransposer>().m_ScreenX = 0.5f;

        if (jumpInput)
            cam.GetCinemachineComponent<CinemachineFramingTransposer>().m_ScreenY = 0.5f;
        else
            cam.GetCinemachineComponent<CinemachineFramingTransposer>().m_ScreenY = 0.7f;
    }
}
