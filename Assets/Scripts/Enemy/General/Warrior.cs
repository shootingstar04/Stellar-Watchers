using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warrior : MonoBehaviour
{
    public enum State
    {
        IDLE,
        PATROL,
        CHASE,
        ATTACK,
        JUMPING,
        FALLING,
        KILLED
    }

    public State currentState;

    public Transform player;
    public float detectionRadius = 10f;
    public float attackRadius = 2f;
    public float moveSpeed = 2f;
    public float patrolDistance = 5f; // 순찰할 거리
    public float jumpForce = 5f; // 점프력

    public float CurHP = 30;

    private Rigidbody2D rb;
    private Animator animator;
    private bool facingRight = true;

    // 추가된 부분: 낭떠러지 및 벽 감지를 위한 변수
    public float cliffDetectionDistance = 1f;
    public float wallDetectionDistance = 0.1f;
    public LayerMask groundLayer;

    private Vector2 startPos;
    private Vector2 patrolLeftLimit;
    private Vector2 patrolRightLimit;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        currentState = State.PATROL;

        startPos = transform.position;
        patrolLeftLimit = new Vector2(startPos.x - patrolDistance, startPos.y);
        patrolRightLimit = new Vector2(startPos.x + patrolDistance, startPos.y);
    }

    private void Update()
    {
        switch (currentState)
        {
            case State.IDLE:
                Idle();
                break;
            case State.PATROL:
                Patrol();
                break;
            case State.CHASE:
                Chase();
                break;
            case State.ATTACK:
                Attack();
                break;
            case State.JUMPING:
                Jumping();
                break;
            case State.FALLING:
                Falling();
                break;
            case State.KILLED:
                Killed();
                break;
        }

        // 상태 전환 로직
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (currentState != State.KILLED && currentState != State.JUMPING && currentState != State.FALLING)
        {
            // 바라보는 방향에서만 플레이어 감지
            Vector2 directionToPlayer = player.position - transform.position;
            if (facingRight && directionToPlayer.x > 0 && directionToPlayer.magnitude <= detectionRadius)
            {
                currentState = State.CHASE;
            }
            else if (!facingRight && directionToPlayer.x < 0 && directionToPlayer.magnitude <= detectionRadius)
            {
                currentState = State.CHASE;
            }

            if (distanceToPlayer <= attackRadius)
            {
                currentState = State.ATTACK;
            }
            else if (currentState == State.CHASE && distanceToPlayer > detectionRadius)
            {
                currentState = State.PATROL;
            }
        }

        // 벽이나 땅의 끝을 감지하면 방향 전환 또는 점프
        if (currentState == State.PATROL)
        {
            if (IsWallAhead())
            {
                Flip();
            }
            else if (IsEdgeAhead())
            {
                if (CanJumpAcross())
                {
                    Jump();
                    return; // 점프한 후에는 다른 작업을 중단
                }
                else
                {
                    Flip();
                }
            }
        }

        // 점프 후 바닥에 닿았는지 확인
        if (currentState == State.JUMPING || currentState == State.FALLING)
        {
            if (IsGrounded())
            {
                currentState = State.PATROL;
                animator.SetBool("Fall", false);
            }
        }
    }

    private void Idle()
    {
        animator.SetBool("Walk", false);
        rb.velocity = Vector2.zero;
    }

    private void Patrol()
    {
        animator.SetBool("Walk", true);

        // 왼쪽 또는 오른쪽 경계에 도달하면 방향 전환
        if (facingRight && transform.position.x >= patrolRightLimit.x)
        {
            Flip();
        }
        else if (!facingRight && transform.position.x <= patrolLeftLimit.x)
        {
            Flip();
        }

        Vector2 target = facingRight ? patrolRightLimit : patrolLeftLimit;
        MoveTo(target);
    }

    private void Chase()
    {
        MoveTo(player.position);
        animator.SetBool("Walk", true);

        // 낭떠러지 감지
        if (IsEdgeAhead())
        {
            if (CanJumpAcross())
            {
                Jump();
                return; // 점프한 후에는 다른 작업을 중단
            }
            else
            {
                Flip();
                currentState = State.PATROL; // 낭떠러지를 피하기 위해 추적을 멈추고 순찰 상태로 변경
            }
        }
    }

    private void Attack()
    {
        rb.velocity = Vector2.zero;
        animator.SetBool("Walk", false);
        animator.SetTrigger("Attack1");

        // 공격 중에도 플레이어와의 거리를 확인
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        if (distanceToPlayer > attackRadius)
        {
            currentState = State.CHASE;
        }

        // 여기서 플레이어에게 데미지를 주는 로직을 추가할 수 있습니다.
    }

    private void Killed()
    {
        rb.velocity = Vector2.zero;
        animator.SetTrigger("Die");

        // 죽음 처리를 여기서 수행합니다. 예: 파괴, 리스폰 등
    }

    private void Jumping()
    {
        if (rb.velocity.y < 0)
        {
            currentState = State.FALLING;
            animator.SetBool("Fall", true);
        }
    }

    private void Falling()
    {
        // 낙하 중일 때 속도를 증가시킵니다.
        rb.velocity += Vector2.up * Physics2D.gravity.y * Time.deltaTime;

        // 낙하 중에 Ground 감지
        if (IsGrounded())
        {
            currentState = State.PATROL;
            animator.SetBool("Fall", false);
            animator.SetBool("Walk", true);
        }
    }

    private void MoveTo(Vector2 target)
    {
        float direction = target.x > transform.position.x ? 1f : -1f;
        Vector2 moveDirection = new Vector2(direction, 0f);
        rb.velocity = new Vector2(moveDirection.x * moveSpeed, rb.velocity.y);
    }

    private void Flip()
    {
        facingRight = !facingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    private bool IsWallAhead()
    {
        Vector2 position = transform.position;
        Vector2 direction = facingRight ? Vector2.right : Vector2.left;
        RaycastHit2D hit = Physics2D.Raycast(position, direction, wallDetectionDistance, groundLayer);

        Debug.DrawRay(position, direction * wallDetectionDistance, Color.blue);

        return hit.collider != null; // 벽이면 true를 반환
    }

    private bool IsEdgeAhead()
    {
        Vector2 position = transform.position;
        Vector2 direction = facingRight ? Vector2.right : Vector2.left;
        float rayDistance = 0.1f;
        RaycastHit2D hit = Physics2D.Raycast(position + direction * rayDistance, Vector2.down, cliffDetectionDistance, groundLayer);

        Debug.DrawRay(position + direction * rayDistance, Vector2.down * cliffDetectionDistance, Color.green);

        return hit.collider == null; // 땅의 끝이면 true를 반환
    }

    private bool CanJumpAcross()
    {
        Vector2 position = transform.position;
        Vector2 rayStart = facingRight ? new Vector2(position.x + 0.5f, position.y - 1.5f) : new Vector2(position.x - 0.5f, position.y - 1.5f);
        Vector2 rayDirection = facingRight ? Vector2.right : Vector2.left;
        float rayDistance = 3f;

        RaycastHit2D hit = Physics2D.Raycast(rayStart, rayDirection, rayDistance, groundLayer);

        Debug.DrawRay(rayStart, rayDirection * rayDistance, Color.yellow);

        return hit.collider != null; // 건너편에 땅이 있는 경우 true 반환
    }

    private void Jump()
    {
        currentState = State.JUMPING;
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        animator.SetTrigger("Jump");
    }

    private bool IsGrounded()
    {
        Vector2 position = transform.position;
        Vector2 direction = facingRight ? Vector2.right : Vector2.left;
        float rayDistance = 0.1f;
        RaycastHit2D hit = Physics2D.Raycast(position + direction * rayDistance, Vector2.down, cliffDetectionDistance, groundLayer);

        Debug.DrawRay(position + direction * rayDistance, Vector2.down * cliffDetectionDistance, Color.red);

        return hit.collider != null; // 바닥에 닿아 있으면 true를 반환
    }

    public void TakeDamage(float damage)
    {
        animator.SetTrigger("Hit");

        CurHP -= damage;

        if (CurHP <= 0)
        {
            currentState = State.KILLED;
        }
    }
}
