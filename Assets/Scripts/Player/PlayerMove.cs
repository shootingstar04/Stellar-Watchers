using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float moveSpeed;
    public float wallSlidingSpeed = 5;
    public float jumpForce;
    public float maxJumpTime;
    public float maxJumpHeight;
    public LayerMask groundLayer;
    public LayerMask wallLayer; // 추가된 부분: 벽 레이어
    public float dashForce = 20f;
    public float dashDuration = 0.2f;
    public float invincibilityDuration = 1f;
    public float fallMultiplier = 2.5f; // 낙하 속도 증가를 위한 계수
    public float wallJumpForce = 10f; // 추가된 부분: 벽 점프 힘
    public float foot_size = 0.7f;

    private Rigidbody2D rigid;
    private bool isGrounded;
    private bool isJumping;
    private bool isDashing;
    private bool canDash;
    private bool isTouchingWall; // 추가된 부분: 벽에 닿았는지 여부
    private bool isWallJump;
    private bool isGrabWall;
    private float jumpTimeCounter;
    private float dashTimeCounter;
    private Transform groundCheck;
    private Transform wallCheck; // 추가된 부분: 벽 감지용 트랜스폼
    private float groundCheckRadius = 0.2f;
    private float wallCheckRadius = 0.2f; // 추가된 부분: 벽 감지 반경
    private float lastDirection = 1f;

    SpriteRenderer spriteRenderer;
    Animator animator;

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        groundCheck = transform.Find("GroundCheck");
        wallCheck = transform.Find("WallCheck"); // 추가된 부분: WallCheck 트랜스폼 찾기
        animator = this.GetComponent<Animator>();
    }

    void Update()
    {
        CheckGround(); //지형 확인
        Move();
        GrabWall();
        Jump();
        Dash();
        AnimationControl();
    }

    void Move()
    {
        if (!isDashing && !isWallJump)
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

            if (Mathf.Abs(rigid.velocity.x) > moveSpeed && rigid.velocity.x * moveInput > 0)
            {
                rigid.velocity = new Vector2(rigid.velocity.x * 0.98f, rigid.velocity.y);
            }
            else
            {
                rigid.velocity = new Vector2(moveInput * moveSpeed, rigid.velocity.y);
            }
        }
    }

    void Jump()
    {

        if ((isGrounded || isTouchingWall) && Input.GetButtonDown("Jump"))
        {
            isJumping = true;
            jumpTimeCounter = 0f;
            rigid.velocity = new Vector2(rigid.velocity.x, jumpForce);
            animator.SetTrigger("Jump");

            if (isTouchingWall && !isGrounded)
            {
                isWallJump = true;
                // 벽 점프 시 플레이어를 반대 방향으로 튕겨내기
                rigid.velocity = new Vector2(-lastDirection * wallJumpForce, jumpForce);
                // 벽 점프 후 방향 전환
                if (lastDirection > 0)
                {
                    transform.localEulerAngles = new Vector3(0, 180, 0);
                }
                else
                {
                    transform.localEulerAngles = new Vector3(0, 0, 0);
                }
                lastDirection = -lastDirection;
            }
        }

        if (isJumping && Input.GetButton("Jump"))
        {
            if (jumpTimeCounter < maxJumpTime)
            {
                jumpTimeCounter += Time.deltaTime;
                float t = jumpTimeCounter / maxJumpTime;
                float currentJumpForce = Mathf.Lerp(jumpForce, maxJumpHeight, t);
                rigid.velocity = new Vector2(rigid.velocity.x, currentJumpForce);

                if (jumpTimeCounter > maxJumpTime / 2)
                {
                    isWallJump = false;
                }
            }
            else
            {
                isJumping = false;
            }
        }

        if (Input.GetButtonUp("Jump"))
        {
            isJumping = false;
            isWallJump = false;
        }

        if (!isGrounded && !isJumping && rigid.velocity.y > 0)
        {
            rigid.velocity = new Vector2(rigid.velocity.x, rigid.velocity.y * 0.5f);
        }

        // 떨어질 때 속도를 증가시키는 로직
        if (rigid.velocity.y < 0)
        {
            rigid.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
    }

    void Dash()
    {
        if (Input.GetKeyDown(KeyCode.C) && canDash)
        {
            isDashing = true;
            isJumping = false;
            isWallJump = false;
            canDash = false;
            dashTimeCounter = dashDuration;
            rigid.velocity = new Vector2(dashForce * lastDirection, 0);
            rigid.gravityScale = 0;
        }

        if (isDashing)
        {
            gameObject.layer = 8;
            dashTimeCounter -= Time.deltaTime;
            if (dashTimeCounter <= 0)
            {
                isDashing = false;
                rigid.velocity = Vector2.zero;
                gameObject.layer = 7;
                rigid.gravityScale = 1;
            }
        }
        else if (isGrounded)
        {
            canDash = true;
        }
    }

    void GrabWall()
    {
        if (isTouchingWall && rigid.velocity.x * (wallCheck.position.x - this.transform.position.x) > 0 )
        {
            isGrabWall = true;
            rigid.gravityScale = 0f;
            rigid.velocity = new Vector2(rigid.velocity.x, -1 * wallSlidingSpeed);
        }

        if (isGrabWall)
        {
            if (!isTouchingWall || isJumping || isDashing)
            {
                rigid.gravityScale = 1;
                isGrabWall = false;
            }
        }

    }
    // 추가된 부분: 벽 감지 함수
    void CheckGround()
    {
        isTouchingWall = Physics2D.OverlapCircle(wallCheck.position, wallCheckRadius, wallLayer);

        RaycastHit2D hit = Physics2D.Raycast((Vector2)groundCheck.position - new Vector2(lastDirection * foot_size, 0.2f), Vector2.right * lastDirection, foot_size * 2, groundLayer);

        isGrounded = hit.collider == null ? false : true;
    }

    void AnimationControl()
    {
        animator.SetFloat("AirSpeedY", rigid.velocity.y);
        animator.SetBool("Grounded", isGrounded);
        animator.SetBool("WallSlide", isGrabWall);
        animator.SetInteger("AnimState", rigid.velocity.x != 0 ? 1 : 0);
    }
}
