using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : MonoBehaviour
{
    public static Skeleton instance;
    public enum State
    {
        IDLE,
        PATROL,
        CHASE,
        ATTACK1,
        ATTACK2,
        SHIELD,
        KILLED
    }

    public State currentState;

    public Transform player;
    public float detectionRadius = 10f;
    public float attackRadius = 4f;
    public float moveSpeed = 5f;
    public float patrolDistance = 5f;

    public float CurHP = 10;

    private Rigidbody2D rb;
    private Animator animator;
    private bool facingRight = true;

    public float cliffDetectionDistance = 2f;
    public float wallDetectionDistance = 1f;
    public LayerMask groundLayer;

    private Vector2 startPos;
    private Vector2 patrolLeftLimit;
    private Vector2 patrolRightLimit;

    private bool isAttacking = false;

    public float skeletonHeight = 2f; // Skeleton의 키 높이

    void Awake()
    {   
        instance = this;
    }

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
        if (isAttacking) return; // 공격 중일 때는 다른 상태로 전환되지 않도록 함

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
            case State.ATTACK1:
                Attack1();
                break;
            case State.ATTACK2:
                Attack2();
                break;
            case State.SHIELD:
                Shield();
                break;
            case State.KILLED:
                Killed();
                break;
        }

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        float heightDifference = Mathf.Abs(transform.position.y - player.position.y);

        if (currentState != State.KILLED && currentState != State.SHIELD)
        {
            Vector2 directionToPlayer = player.position - transform.position;

            if (currentState != State.ATTACK1 && currentState != State.ATTACK2) // 공격 중이 아닐 때만 상태 전환
            {
                if (heightDifference <= skeletonHeight) // 플레이어가 Skeleton의 키 높이 내에 있을 때만 감지
                {
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
                        if (player.GetComponent<Rigidbody2D>().velocity.y > 0 && Random.value < 0.3f) // 30% 확률로 Attack2 발동
                        {
                            currentState = State.ATTACK2;
                        }
                        else
                        {
                            currentState = State.ATTACK1;
                        }
                    }
                    else if (currentState == State.CHASE && distanceToPlayer > detectionRadius)
                    {
                        currentState = State.PATROL;
                    }
                }
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

    private void Attack1()
    {
        StartCoroutine(AttackCoroutine("Attack1"));
    }

    private void Attack2()
    {
        StartCoroutine(AttackCoroutine("Attack2"));
    }

    private IEnumerator AttackCoroutine(string attackTrigger)
    {
        isAttacking = true;
        rb.velocity = Vector2.zero; // 공격 중 이동하지 않음
        animator.SetBool("Walk", false);
        animator.SetTrigger(attackTrigger);

        yield return new WaitForSeconds(1f); // 공격 애니메이션 재생 시간 대기

        yield return new WaitForSeconds(1f); // 공격 후 1초 딜레이

        isAttacking = false;

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        if (distanceToPlayer > attackRadius)
        {
            currentState = State.CHASE;
        }
    }

    private void Shield()
    {
        rb.velocity = Vector2.zero;
        animator.SetBool("Walk", false);
        animator.SetTrigger("Shield");

        StartCoroutine(ShieldDuration());
    }

    private IEnumerator ShieldDuration()
    {
        yield return new WaitForSeconds(2f); // 방어 행동 지속 시간
        currentState = State.PATROL;
    }

    private void Killed()
    {
        rb.velocity = Vector2.zero;
        animator.SetTrigger("Die");
        
        Destroy(gameObject, 1f);
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

    public void TakeDamage(float damage)
    {
        animator.SetTrigger("Hit");
        CurHP -= damage;

        if (CurHP <= 0)
        {
            currentState = State.KILLED;
        }
        else if (IsFacingPlayer() && Random.value < 0.1f) // 10% 확률로 방어 행동
        {
            currentState = State.SHIELD;
        }
    }

    private bool IsFacingPlayer()
    {
        Vector2 directionToPlayer = player.position - transform.position;
        return (facingRight && directionToPlayer.x > 0) || (!facingRight && directionToPlayer.x < 0);
    }
}
