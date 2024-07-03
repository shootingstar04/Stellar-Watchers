using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private float jumpForce = 10f;
    private float jumpTimeCounter;
    private float maxJumpTime = 0.5f;
    private float maxDbJumpTime = 0.3f;
    private float maxJumpHeight = 5f;
    private float maxDbJumpHeight = 3f;
    private bool dbJumpCount = true;

    private float moveSpeed = 5f;
    private float lastDirection = 1f;

    private float dashSpeed = 20f;
    private float dashDuration = 0.2f;
    private float dashCoolDown = 0.1f;

    private float invinDuration = 1f;

    private bool isJumping = false;
    private bool isDbJumping = false;
    private bool isGrounded = true;
    private bool isDashing = false;
    private bool isInvins = false;

    private float dashTimeCounter;
    private float invincibilityCounter;

    private Collider2D col;
    private Rigidbody2D rigid;

    [SerializeField] private LayerMask Jumpables;
    private Transform jumpCheck;
    private float groundCheckRadius = 0.2f;

    SpriteRenderer spriteRenderer;

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        jumpCheck = transform.Find("GroundCheck");

    }

    void Update()
    {
        Move();
        Jump();
    }
    void Move()
    {
        if (!isDashing)
        {
            float moveInput = Input.GetAxis("Horizontal");

            if (moveInput != 0)
            {
                lastDirection = moveInput > 0 ? 1f : -1f;
            }

            if (moveInput < 0)
            {
                transform.localEulerAngles = new Vector3(0, 180, 0);
            }
            else if (moveInput > 0)
            {
                transform.localEulerAngles = new Vector3(0, 0, 0);
            }

            rigid.velocity = new Vector2(moveInput * moveSpeed, rigid.velocity.y);
        }
    }

    private void Jump()
    {
        //점프를 2가지로 구분 - 기본 점프, 2단(해금)점프
        //점프의 조건을 구분
        //기본 : 땅에 닿았을 때 1번 가능. 이후 다시 닿았다면 초기화 <<- isGrounded가 true일 때만
        //2단  : 해금 필요 && 공중에 있을 때 1번 가능. 이후 어떻게든 다시 공중에 있다면 초기화
        isGrounded = Physics2D.OverlapCircle(jumpCheck.position, groundCheckRadius, Jumpables); //수직 속도가 0일때만 가능하게 하는 조건이 필요한가 -- 이를태면 떨어지는 도중 옆에 스쳤다 - 이때도 점프 초기화 o/x의 문제?

        if (isGrounded) //이부분 맘에 안드는데... - 생각나는 것이 없음 --> 이걸 통해 점프 후 2단점프 포함, 높은곳에서 떨어질 때 2단점프도 가능하게 하였음 - 땅밟으면 2단점프 카운트 초기화.(true가 점프 가능한 상태 - 굳이 int형 안썼음.) 
        {
            dbJumpCount = true;
        }

        //---------기본점프----------------

        if (isGrounded && Input.GetKeyDown(KeyCode.Z))                          //땅밟은 상태 && 키 눌렀을 때(낮점)
        {
            Debug.Log("기본점프");
            isJumping = true;                                                  //점프 중
            jumpTimeCounter = 0f;                                              //maxJump까지 계산하는 타이머
            rigid.velocity = new Vector2(rigid.velocity.x, jumpForce);
        }

        if (isJumping && Input.GetKey(KeyCode.Z))                               //점프중일때만 && 키 눌린 상태(높점)
        {
            if (jumpTimeCounter < maxJumpTime)                                  //타이머가 maxJump미만일때만
            {
                Debug.Log("기본연장");
                jumpTimeCounter += Time.deltaTime;
                float t = jumpTimeCounter / maxJumpTime;
                float currentJumpForce = Mathf.Lerp(jumpForce, maxJumpHeight, t);
                rigid.velocity = new Vector2(rigid.velocity.x, currentJumpForce);
            }
            else                                                                //타이머가 넘어가면
            {
                isJumping = false;                                              //높점 조건 파기
            }
        }

        if (isJumping && Input.GetKeyUp(KeyCode.Z))                             //또한 점프중일때(높점중) && 누르던 키를 떼면
        {
            isJumping = false;                                                  //높점 조건 파기
        }

        if (!isGrounded && !isJumping && rigid.velocity.y > 0)                  //떨어질때? - 공중에서 && 점프중(높점중)아님 && y상승중일때 <-- 아마 높점 파기 후 가속 중단 부분일듯
        {
            rigid.velocity = new Vector2(rigid.velocity.x, rigid.velocity.y * 0.5f); //1/2씩 y속력 감소
        }

        //-------------2단점프---------------------

        if (!isGrounded && Input.GetKeyDown(KeyCode.Z) && dbJumpCount)         //공중에 있을 때 && 키 눌렀을 때 && 점프카운트 가능할 때(낮점)
                                                                               //추가로 여기에다 && (해금 조건 확인) 추가할 것
        {
            Debug.Log("2단점프");
            jumpTimeCounter = 0;                                               //타이머 초기화 - 둘이 안겹쳐서 - 어차피 이거 발동하면 이전입력 덮어씌우는거니까 - 재활용해도 상관없는듯
            isDbJumping = true;                                                //더블점프중
            dbJumpCount = false;                                               //점프카운트 감소 (1 -> 0)
            rigid.velocity = new Vector2(rigid.velocity.x, jumpForce);
        }

        if (isDbJumping && Input.GetKey(KeyCode.Z))                            //더블점프중 && 키 누르는 중(높점)
        {
            if (jumpTimeCounter < maxDbJumpTime)                               //타이머가 maxDbJump미만일때만
            {
                Debug.Log("2단연장");
                jumpTimeCounter += Time.deltaTime;
                float t = jumpTimeCounter / maxDbJumpTime;
                float currentJumpForce = Mathf.Lerp(jumpForce, maxDbJumpHeight, t);
                rigid.velocity = new Vector2(rigid.velocity.x, currentJumpForce);
            }
            else                                                               //타이머가 넘어가면
            {
                isDbJumping = false;                                           //높점조건 파기
            }
        }

        if (isDbJumping && Input.GetKeyUp(KeyCode.Z))                          //높점중이더라도 && 키를 떼면
        {
            isDbJumping = false;                                               //높점 조건 파기
        }

        if (!isGrounded && !isDbJumping && rigid.velocity.y > 0)               //이것도 높점도중 끝난다면 속도 감속시키는 부분일듯
        {
            rigid.velocity = new Vector2(rigid.velocity.x, rigid.velocity.y * 0.5f);
        }

    }

    void Dash()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isDashing)
        {
            isDashing = true;
            isInvins = true;
            dashTimeCounter = dashDuration;
            rigid.velocity = new Vector2(dashSpeed * lastDirection, rigid.velocity.y);

        }
        if (isDashing)
        {
            gameObject.layer = 8;
            dashTimeCounter -= Time.deltaTime;
            if (dashTimeCounter <= 0)
            {
                isInvins = false;
                dashTimeCounter = dashCoolDown;
                dashTimeCounter -= Time.deltaTime;
                if (dashTimeCounter <= 0)
                {
                    isDashing = false;
                }
            }
        }
    }
}