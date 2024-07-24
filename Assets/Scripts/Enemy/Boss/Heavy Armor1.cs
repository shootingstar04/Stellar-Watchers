using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeavyArmor1 : MonoBehaviour
{
    public enum State
    {
        IDLE,
        PATROL,
        CHASE,
        ATTACK1,
        ATTACK2,
        ATTACK3,
        SKILL1,
        SKILL2,
        SKILL3,
        HIT,
        KILLED
    }

    public State currentState;

    public Transform player;
    public float detectionRadius = 15f;
    public float attackRadius = 5f;
    public float moveSpeed = 3f;
    public float patrolDistance = 7f;
    public float skill1MoveDistance = 1f; // 이동 거리
    public float skill1MoveInterval = 0.4f; // 이동 간격
    public int skill1TotalMoves = 9; // 총 이동 횟수

    public int CurHP = 50;

    private Rigidbody2D rb;
    private Animator animator;
    private bool facingRight = true;

    public float cliffDetectionDistance = 2f;
    public float wallDetectionDistance = 1f;
    public LayerMask groundLayer;

    private Vector2 startPos;
    private Vector2 patrolLeftLimit;
    private Vector2 patrolRightLimit;

    private bool isPerformingAction = false;

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
        if (isPerformingAction) return; // 행동 중일 때는 다른 상태로 전환되지 않도록 함

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
            case State.HIT:
                Hit();
                break;
            case State.KILLED:
                Killed();
                break;
        }

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (currentState != State.KILLED && !isPerformingAction)
        {
            if (distanceToPlayer <= attackRadius)
            {
                StartCoroutine(RandomAttackOrSkill());
            }
            else if (distanceToPlayer <= detectionRadius)
            {
                currentState = State.CHASE;
            }
            else if (currentState != State.PATROL)
            {
                currentState = State.IDLE;
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
        // 이동할 방향 결정
        float direction = player.position.x > transform.position.x ? 1f : -1f;

        // 현재 facingRight와 이동할 방향 비교
        if ((direction > 0 && !facingRight) || (direction < 0 && facingRight))
        {
            Flip();
        }   

        MoveTo(player.position);
        animator.SetBool("Walk", true);

        if (IsEdgeAhead())
        {
            Flip();
            currentState = State.PATROL;
        }
    }

    private IEnumerator PerformAction(State actionState, float actionDuration)
    {
        isPerformingAction = true;
        rb.velocity = Vector2.zero;
        animator.SetBool("Walk", false);

        switch (actionState)
        {
            case State.ATTACK1:
                Debug.Log("A-1");
                animator.SetTrigger("Attack1");
                break;
            case State.ATTACK2:
                Debug.Log("A-2");
                animator.SetTrigger("Attack2");
                break;
            case State.ATTACK3:
                Debug.Log("A-3");
                animator.SetTrigger("Attack3");
                break;
            case State.SKILL1:
                yield return StartCoroutine(PerformSkill1());
                break;
            case State.SKILL2:
                Debug.Log("S-2");
                animator.SetTrigger("Skill2");
                break;
            case State.SKILL3:
                Debug.Log("S-3");
                animator.SetTrigger("Skill3");
                break;
        }

        yield return new WaitForSeconds(actionDuration); // 행동 애니메이션 시간 대기

        isPerformingAction = false;
        currentState = State.IDLE;
    }

    private IEnumerator PerformSkill1()
    {
        animator.SetTrigger("Skill1");

        Vector2 initialPosition = transform.position;
        Vector2 targetPosition = player.position;

        for (int i = 0; i < skill1TotalMoves; i++)
        {
            // 플레이어 방향으로 이동
            Vector2 moveDirection = (targetPosition - initialPosition).normalized;
            transform.position += (Vector3)moveDirection * skill1MoveDistance;

            yield return new WaitForSeconds(skill1MoveInterval);
        }
    }

    private IEnumerator RandomAttackOrSkill()
    {
        isPerformingAction = true;
        yield return new WaitForSeconds(3f); // 3초 대기

        float randomValue = Random.value;
        if (randomValue < 0.166f)
        {
            currentState = State.ATTACK1;
            StartCoroutine(PerformAction(State.ATTACK1, 1f));
        }
        else if (randomValue < 0.333f)
        {
            currentState = State.ATTACK2;
            StartCoroutine(PerformAction(State.ATTACK2, 1f));
        }
        else if (randomValue < 0.5f)
        {
            currentState = State.ATTACK3;
            StartCoroutine(PerformAction(State.ATTACK3, 1f));
        }
        else if (randomValue < 0.666f)
        {
            currentState = State.SKILL1;
            StartCoroutine(PerformAction(State.SKILL1, 2f));
        }
        else if (randomValue < 0.833f)
        {
            currentState = State.SKILL2;
            StartCoroutine(PerformAction(State.SKILL2, 2f));
        }
        else
        {
            currentState = State.SKILL3;
            StartCoroutine(PerformAction(State.SKILL3, 2f));
        }
    }

    private void Hit()
    {
        animator.SetTrigger("Hit");
        currentState = State.IDLE;
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

        if (CurHP <= 0)
        {
            currentState = State.KILLED;
        }
        else
        {
            currentState = State.HIT;
        }
    }
}