using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class cam_deadzone_test : MonoBehaviour
{
    private CinemachineVirtualCamera cam;
<<<<<<< HEAD
=======
    private CinemachineFramingTransposer camT;

    float lastMinput;

>>>>>>> af53cb1c83a19e62ef889f90a2f9eb2fb512ccdd

    private void Start()
    {
        cam = GetComponent<CinemachineVirtualCamera>();
<<<<<<< HEAD
=======
        camT = cam.GetCinemachineComponent<CinemachineFramingTransposer>();
>>>>>>> af53cb1c83a19e62ef889f90a2f9eb2fb512ccdd
    }

    private void Update()
    {
<<<<<<< HEAD
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
=======
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

>>>>>>> af53cb1c83a19e62ef889f90a2f9eb2fb512ccdd
