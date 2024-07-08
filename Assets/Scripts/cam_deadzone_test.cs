using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class cam_deadzone_test : MonoBehaviour
{
    private CinemachineVirtualCamera cam;
    private CinemachineFramingTransposer camT;

    float lastMinput;


    private void Start()
    {
        cam = GetComponent<CinemachineVirtualCamera>();
        camT = cam.GetCinemachineComponent<CinemachineFramingTransposer>();
    }

    private void Update()
    {
        float moveInput = Input.GetAxisRaw("Horizontal");
        float sightInput = Input.GetAxisRaw("Vertical");

        bool jumpInput = Input.GetKey(KeyCode.Z);

        if (moveInput != 0)
            lastMinput = moveInput;

        camT.m_ScreenX = 0.5f - (lastMinput * 0.05f);
        /*
        if (moveInput == 1f)
            camT.m_ScreenX = 0.45f;
        else if (moveInput == -1f)
            camT.m_ScreenX = 0.55f;
        else if (moveInput == 0f)
            camT.m_ScreenX = 0.5f - lastMinput * 0.05f;
        */

        //논리구조 오류 조심 -- 점프와 위아래 화살표 사이 안겹치게
        if (jumpInput && sightInput == 0)
            camT.m_ScreenY = 0.65f;
        else if (!jumpInput && sightInput == 0)
            camT.m_ScreenY = 0.55f;
        else if (sightInput == 1f)
            camT.m_ScreenY = 1.0f;
        else if (sightInput == -1f)
            camT.m_ScreenY = 0.3f;
        else
            camT.m_ScreenY = 0.5f;


    }
}

