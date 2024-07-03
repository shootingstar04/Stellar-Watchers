using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float moveSpeed;
    public float jumpForce;
    public float maxJumpTime;
    public float maxJumpHeight;
    public LayerMask groundLayer;
    public float dashForce = 20f;
    public float dashDuration = 0.2f;
    public float invincibilityDuration = 1f;

    private Rigidbody2D rigid;
    private bool isGrounded;
    private bool isJumping;
    private bool isDashing;
    private bool isInvincible;
    private float jumpTimeCounter;
    private float dashTimeCounter;
    private float invincibilityCounter;
    private Transform groundCheck;
    private float groundCheckRadius = 0.2f;
    private float lastDirection = 1f;

    SpriteRenderer spriteRenderer;

    public Collider2D Pc;

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        groundCheck = transform.Find("GroundCheck");

        Pc = this.GetComponent<Collider2D>();
    }

    void Update()
    {
        Move();
        Jump();
        Dash();
        HandleInvincibility();
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

            rigid.velocity = new Vector2(moveInput * moveSpeed, rigid.velocity.y);
        }
    }

    void Jump()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            isJumping = true;
            jumpTimeCounter = 0f;
            rigid.velocity = new Vector2(rigid.velocity.x, jumpForce);
        }

        if (isJumping && Input.GetButton("Jump"))
        {
            if (jumpTimeCounter < maxJumpTime)
            {
                jumpTimeCounter += Time.deltaTime;
                float t = jumpTimeCounter / maxJumpTime;
                float currentJumpForce = Mathf.Lerp(jumpForce, maxJumpHeight, t);
                rigid.velocity = new Vector2(rigid.velocity.x, currentJumpForce);
            }
            else
            {
                isJumping = false;
            }
        }

        if (Input.GetButtonUp("Jump"))
        {
            isJumping = false;
        }

        if (!isGrounded && !isJumping && rigid.velocity.y > 0)
        {
            rigid.velocity = new Vector2(rigid.velocity.x, rigid.velocity.y * 0.5f);
        }
    }

    void Dash()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isDashing)
        {
            isDashing = true;
            isInvincible = true;
            dashTimeCounter = dashDuration;
            rigid.velocity = new Vector2(dashForce * lastDirection, rigid.velocity.y);
        }

        if (isDashing)
        {
            gameObject.layer = 8;
            dashTimeCounter -= Time.deltaTime;
            if (dashTimeCounter <= 0)
            {
                isDashing = false;
                isInvincible = false;
                gameObject.layer = 7;
            }
        }
    }

    void HandleInvincibility()
    {
        if (isInvincible && !isDashing)
        {
            gameObject.layer = 8;
            spriteRenderer.color = new Color(1, 1, 1, 0.4f);

            invincibilityCounter -= Time.deltaTime;
            if (invincibilityCounter <= 0)
            {
                isInvincible = false;
                gameObject.layer = 7;
                spriteRenderer.color = new Color(1, 1, 1, 1);
            }
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Enemy" && !isInvincible)
        {
            Debug.Log("Player hit");
            isInvincible = true;
            invincibilityCounter = invincibilityDuration;
        }
    }
}
