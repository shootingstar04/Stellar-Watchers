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

    private Rigidbody2D rb;
    private bool isGrounded;
    private bool isJumping;
    private float jumpTimeCounter;
    private Transform groundCheck;
    private float groundCheckRadius = 0.2f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        groundCheck = transform.Find("GroundCheck");
    }

    void Update()
    {
        Move();
        Jump();
    }

    void Move()
    {
        float moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);
    }

    void Jump()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            isJumping = true;
            jumpTimeCounter = 0f;
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        if (isJumping && Input.GetButton("Jump"))
        {
            if (jumpTimeCounter < maxJumpTime)
            {
                jumpTimeCounter += Time.deltaTime;
                float t = jumpTimeCounter / maxJumpTime;
                float currentJumpForce = Mathf.Lerp(jumpForce, maxJumpHeight, t);
                rb.velocity = new Vector2(rb.velocity.x, currentJumpForce);
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

        if (!isGrounded && !isJumping && rb.velocity.y > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }
    }
}
