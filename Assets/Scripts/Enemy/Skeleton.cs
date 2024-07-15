using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : MonoBehaviour
{
    public enum State
    {
        IDLE,
        PATROL,
        CHASE,
        ATTACK,
        KILLED
    }

    public State currentState;

    public Transform player;
    public float detectionRadius = 10f;
    public float attackRadius = 4f;
    public float moveSpeed = 5f;
    public float patrolDistance = 5f;

    public int CurHP = 10;

    private Rigidbody2D rb;
    private Animator animator;
    private bool facingRight = true;

    public float cliffDetectionDistance = 2f;
    public float wallDetectionDistance = 1f;
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
            case State.KILLED:
                Killed();
                break;
        }

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (currentState != State.KILLED)
        {
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

        if (currentState == State.PATROL)
        {
            if (IsWallAhead() || IsEdgeAhead())
            {
                Flip();
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

        if (IsNearCliff())
        {
            Flip();
            currentState = State.PATROL;
        }
    }

    private void Attack()
    {
        rb.velocity = Vector2.zero;
        animator.SetBool("Walk", false);
        animator.SetTrigger("Attack1");

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        if (distanceToPlayer > attackRadius)
        {
            currentState = State.CHASE;
        }

    }

    private void Killed()
    {
        rb.velocity = Vector2.zero;
        animator.SetTrigger("Die");

        // 죽음 처리를 여기서 수행합니다. 예: 파괴, 리스폰 등
    }

    private void MoveTo(Vector2 target)
    {
        float direction = target.x > transform.position.x ? 1f : -1f;
        Vector2 moveDirection = new Vector2(direction, 0f);
        rb.velocity = moveDirection * moveSpeed;
    }

    private void Flip()
    {
        facingRight = !facingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    private bool IsNearCliff()
    {
        Vector2 position = transform.position;
        Vector2 rayStart = facingRight ? new Vector2(position.x + 0.5f, position.y) : new Vector2(position.x - 0.5f, position.y);

        RaycastHit2D hit = Physics2D.Raycast(rayStart, Vector2.down, cliffDetectionDistance, groundLayer);

        Debug.DrawRay(rayStart, Vector2.down * cliffDetectionDistance, Color.red);

        return hit.collider == null;
    }

    private bool IsWallAhead()
    {
        Vector2 position = transform.position;
        Vector2 direction = facingRight ? Vector2.right : Vector2.left;
        RaycastHit2D hit = Physics2D.Raycast(position, direction, wallDetectionDistance, groundLayer);

        Debug.DrawRay(position, direction * wallDetectionDistance, Color.blue);

        return hit.collider != null;
    }

    private bool IsEdgeAhead()
    {
        Vector2 position = transform.position;
        Vector2 direction = facingRight ? Vector2.right : Vector2.left;
        float rayDistance = 0.1f;
        RaycastHit2D hit = Physics2D.Raycast(position + direction * rayDistance, Vector2.down, cliffDetectionDistance, groundLayer);

        Debug.DrawRay(position + direction * rayDistance, Vector2.down * cliffDetectionDistance, Color.green);

        return hit.collider == null;
    }

    public void TakeDamage(int damage)
    {
        animator.SetTrigger("Hit");
        CurHP -= damage;

        Flip();
    }
}
