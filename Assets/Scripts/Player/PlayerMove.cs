using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    public float dashCool = 0.2f;
    public float invincibilityDuration = 1f;
    public float fallMultiplier = 2.5f; // 낙하 속도 증가를 위한 계수
    public float wallJumpForce = 10f; // 추가된 부분: 벽 점프 힘
    public float foot_size = 0.5f;
    public float hittedForceX = 10f;
    public float hittedForceY = 15f;
    public float hittedTime = 0.3f;
    public float hittedDelay1 = 0.3f;
    public float hittedDelay2 = 0.3f;

    [HideInInspector]
    public bool isAttacking;

    private Rigidbody2D rigid;
    private bool isGrounded;
    private bool isJumping;
    private int canJump;
    private bool isDashing;
    private bool canDash;
    private bool isTouchingWall; // 추가된 부분: 벽에 닿았는지 여부
    private bool isWallJump;
    private bool isGrabWall;
    private bool ispause;
    private bool isUsingSkill;
    private bool isDead = false;
    private bool isHitted;
    private float hittedCounter;
    private float jumpTimeCounter;
    private float dashTimeCounter;
    private float dashCoolCounter;
    private Transform groundCheck;
    private Transform wallCheck; // 추가된 부분: 벽 감지용 트랜스폼
    private Transform headingCheck; // 추가된 부분: 천장 감지용 트랜스폼
    private float wallCheckRadius = 0.2f; // 추가된 부분: 벽 감지 반경
    private float lastDirection = 1f;

    SpriteRenderer spriteRenderer;
    Animator animator;

    public enum animationType
    {
        idle = 0,
        block,
        attack1,
        attack2,
        attack3
    }

    public bool IsJumping
    {
        get
        {
            return isJumping;
        }
    }
    public bool IsGrounded
    {
        get
        {
            return isGrounded;
        }
    }
    public bool IsDashing
    {
        get
        {
            return isDashing;
        }
    }
    public bool IsGrabWall
    {
        get
        {
            return isGrabWall;
        }
    }
    public bool IsPause
    {
        get
        {
            return ispause;
        }
    }
    public float LastDirection
    {
        get
        {
            return lastDirection;
        }
    }

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        groundCheck = transform.Find("GroundCheck");
        wallCheck = transform.Find("WallCheck"); // 추가된 부분: WallCheck 트랜스폼 찾기
        headingCheck = transform.Find("HeadingCheck"); // 추가된 부분: HeadingCheck 트랜스폼 찾기
        animator = this.GetComponent<Animator>();
    }

    void Update()
    {
        if (!(Time.timeScale == 0 || ispause || isUsingSkill))
        {
            Move();
            GrabWall();
            Jump();
            Dash();
            AnimationControl();
            CheckGround();//지형 확인
        }
        if (isUsingSkill)
        {
            CheckGround();
        }
        Death();
    }

    void Move()
    {
        if (isHitted)
        {
            hittedCounter += Time.deltaTime;
            if (hittedCounter > hittedTime)
            {
                isHitted = false;
            }
        }
        else
        {
            hittedCounter = 0;
        }

        float moveInput = Input.GetAxis("Horizontal");

        if (!isAttacking)
        {
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
        }

        if (!isDashing && !isWallJump && !isHitted)
        {

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
        if ((isGrounded || isGrabWall))
        {
            canJump = ProgressData.Instance.Acquired[2] ? 2 : 1;
        }

        if (!isGrounded && !isGrabWall && !isJumping)
        {
            if (ProgressData.Instance.Acquired[2] && canJump == 2)
            {
                canJump = 1;
            }
            else if (!ProgressData.Instance.Acquired[2] && canJump == 1)
            {
                canJump = 0;
            }
        }

        if (canJump > 0 && Input.GetButtonDown("Jump"))
        {
            isHitted = false;
            isJumping = true;
            jumpTimeCounter = 0f;
            rigid.velocity = new Vector2(rigid.velocity.x, jumpForce);
            animator.SetTrigger("Jump");

            if (isTouchingWall && !isGrounded && ProgressData.Instance.Acquired[1])
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
                canJump -= 1;
            }
        }

        if (isJumping && !Input.GetButton("Jump"))
        {
            isJumping = false;
            isWallJump = false;
            canJump -= 1;
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
        if (ProgressData.Instance.Acquired[0])
        {
            if (Input.GetKeyDown(KeyCode.C) && canDash)
            {
                isHitted = false;
                isDashing = true;
                isJumping = false;
                isWallJump = false;
                canDash = false;
                dashTimeCounter = dashDuration;
                if (Input.GetAxis("Horizontal") != 0)
                {
                    rigid.velocity = new Vector2(dashForce * (Input.GetAxis("Horizontal") > 0 ? 1 : -1), 0);
                }
                else
                {
                    rigid.velocity = new Vector2(dashForce * lastDirection, 0);
                }
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
            else if ((isGrounded || isGrabWall) && dashCoolCounter > dashCool)
            {
                dashCoolCounter = 0;
                canDash = true;
            }

            if (!canDash)
            {
                dashCoolCounter += Time.deltaTime;
            }
        }
    }

    void GrabWall()
    {

        if (ProgressData.Instance.Acquired[1])
        {
            if (isTouchingWall && rigid.velocity.x * (wallCheck.position.x - this.transform.position.x) > 0)
            {
                isGrabWall = true;
                rigid.gravityScale = 0f;
                rigid.velocity = new Vector2(rigid.velocity.x, -1 * wallSlidingSpeed);
            }

            if (isGrabWall)
            {
                if (!isTouchingWall || isJumping || isDashing || ispause)
                {
                    rigid.gravityScale = 1;
                    isGrabWall = false;
                }
            }
        }
    }
    // 추가된 부분: 지형 감지 함수
    void CheckGround()
    {
        Collider2D wall = Physics2D.OverlapCircle(wallCheck.position, wallCheckRadius, wallLayer);
        if (wall != null && !wall.isTrigger)
        {
            isTouchingWall = true;
            isHitted = false;
        }
        else
        {
            isTouchingWall = false;
        }
        RaycastHit2D foothit = Physics2D.Raycast((Vector2)groundCheck.position - new Vector2(lastDirection * foot_size / 2, 0), Vector2.right * lastDirection, foot_size, groundLayer);

        RaycastHit2D headhit = Physics2D.Raycast((Vector2)headingCheck.position - new Vector2(lastDirection * foot_size / 2, 0), Vector2.right * lastDirection, foot_size, groundLayer);

        //Debug.Log(foothit.collider + " " + headhit.collider);

        isGrounded = foothit.collider != null && !foothit.collider.isTrigger && !isJumping ? true : false;

        if (IsJumping && (headhit.collider != null && !headhit.collider.isTrigger && !headhit.collider.gameObject.CompareTag(Define.OneWayTag)))
        {
            isJumping = false;
            isWallJump = false;
            canJump -= 1;
        }
    }


    // 추가된 부분: 애니매이션 재생 함수
    void AnimationControl()
    {
        animator.SetFloat("AirSpeedY", rigid.velocity.y);
        animator.SetBool("Grounded", isGrounded);
        animator.SetBool("WallSlide", isGrabWall);
        animator.SetInteger("AnimState", rigid.velocity.x != 0 ? 1 : 0);
    }
    void Death()
    {
        if (PlayerHealth.instance.CurHP < 1 && !isDead)
        {
            animator.SetTrigger("Death");
            SceneTransition.instance.FadeOut();
            isDead = true;
            PauseMove();

            DeathScreen.Instance.ShowDeathScreen();
        }
    }

    public void respawn()
    {
        isDead = false;

        DontDestroy.thisIsPlayer.isTeleported = false;

        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            Debug.Log("first0");
            PlayerPrefs.SetInt(Define.sceneIndex, 2);
        }

        SaveLoadManager.instance.LoadPlayerData();
        PlayerHealth.instance.modify_HP(99);
        SaveLoadManager.instance.LoadMapData();
        SceneTransition.instance.FadeIn();
        RestartMove();

    }

    public void Hitted(float force)
    {
        Debug.Log("hit호출");
        isHitted = true;

        animator.SetTrigger("Hurt");


        ParticleManager.instance.particle_generation(ParticleManager.particleType.Hitted, this.transform.position);

        Debug.Log("플레이어 피격 진동효과");
        GetComponent<ImpulseSource>().ShakeEffect();

        StartCoroutine(hit_delay(hittedDelay1, hittedDelay2));



        Collider2D[] hitted = Physics2D.OverlapCircleAll(this.transform.position, 5);

        foreach (Collider2D collider in hitted)
        {
            if (collider.CompareTag(Define.EnemyTag))
            {
                int dir;

                if (collider.transform.position.x - this.transform.position.x > 0)
                {
                    dir = -1;
                }
                else
                {
                    dir = 1;
                }

                if (!(ispause || isUsingSkill)) 
                    rigid.velocity = new Vector2(hittedForceX * dir * force, hittedForceY);
            }
        }
    }

    public void PlayAnimation(animationType type, float isPlay)
    {
        switch (type)
        {
            case animationType.block:
                animator.SetBool("Block", isPlay == 0 ? false : true);
                animator.SetBool("Grounded", isPlay == 0 ? false : true);
                break;
        }
    }
    public void PauseMove()
    {
        rigid.velocity = Vector2.zero;
        ispause = true;
        GrabWall();
        AnimationControl();
    }
    public void RestartMove()
    {
        ispause = false;
    }
    public void UseSkill(bool isStopMove)
    {
        rigid.velocity = Vector2.zero;
        rigid.gravityScale = 1;

        isJumping = false;
        isWallJump = false;
        isDashing = false;
        isGrabWall = false;

        isUsingSkill = isStopMove;
    }

    IEnumerator hit_delay(float delay1, float delay2)
    {
        yield return new WaitForSecondsRealtime(delay1);

        Time.timeScale = 0;

        yield return new WaitForSecondsRealtime(delay2);

        Time.timeScale = 1;


    }
}
