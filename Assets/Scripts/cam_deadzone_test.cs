using Cinemachine;
using UnityEngine;

public class cam_deadzone_test : MonoBehaviour
{
    private CinemachineVirtualCamera cam;
    private CinemachineFramingTransposer camT;

    float lastMinput;
    bool isAttacking;
    private Vector3 priviousPos;
    private float vel;

    float moveInput;
    float sightInput;
    bool jumpInput;
    bool lookAround;

    float timer = 0f;

    float height;
    float width;

    float lx;
    float ly;

    public Vector2 mapSize;

    private void Start()
    {
        cam = GetComponent<CinemachineVirtualCamera>();
        camT = cam.GetCinemachineComponent<CinemachineFramingTransposer>();
        priviousPos = this.transform.position;

        height = cam.m_Lens.OrthographicSize;
        width = height * Screen.width / Screen.height;

        lx = mapSize.x - width;
        ly = mapSize.y - height;

    }

    private void Update()
    {

        moveInput = Input.GetAxisRaw("Horizontal");
        sightInput = Input.GetAxisRaw("Vertical");

        jumpInput = Input.GetKey(KeyCode.Z);
        isAttacking = Input.GetKey(KeyCode.X);


        if (sightInput != 0f)
        {
            timer += Time.deltaTime;
            if (timer >= 1f)
            {
                    timer = 0f;
                    lookAround = true;
            }
        }

        if(sightInput== 0f || moveInput != 0 || isAttacking || jumpInput)
        {
            timer = 0f;
            lookAround = false;
        }

        Vector3 currentPos = transform.position;
        vel = (currentPos - priviousPos).magnitude / Time.deltaTime;
        priviousPos = currentPos;

        if (moveInput != 0)
            lastMinput = moveInput;

        camT.m_ScreenX = 0.5f - (lastMinput * 0.05f);

        //논리구조 오류 조심 -- 점프와 위아래 화살표 사이 안겹치게
        //공격, 이동중 시야이동 안되게...
        if (jumpInput)
            camT.m_ScreenY = 0.65f;

        else if (!jumpInput)
        {
            camT.m_ScreenY = 0.55f;

            if (moveInput == 0)
                cameracontol();
        }
    }

    private void cameracontol()
    {
        if (!isAttacking && lookAround && sightInput == 1f)
            camT.m_ScreenY = 0.8f;

        else if (!isAttacking && lookAround && sightInput == -1f)
            camT.m_ScreenY = 0.3f;

        else if (lookAround && sightInput == 0f)
            camT.m_ScreenY = 0.5f;
    }

    void ScreenBoundary()
    {

    }
}
