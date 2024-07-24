using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallenKnight : MonoBehaviour
{
    public enum State
    {
        IDLE,
        PATROL,
        CHASE,
        ATTACK1,
        ATTACK2,
        ATTACK3,
        SKILL,
        ROLL,
        BLOCK,
        SUMMONS,
        HIT,
        KILLED
    }

    public State currentState;

    public Transform player;
    public float detectionRadius = 20f;
    public float attackRadius = 3f;
    public float moveSpeed = 2f;
    public float patrolDistance = 10f;
    public float decisionInterval = 2f; // 의사 결정 간격

    public int CurHP = 100;

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
    private float nextDecisionTime = 0f; // 다음 의사 결정을 위한 시간

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

        if (Time.time >= nextDecisionTime)
        {
            nextDecisionTime = Time.time + decisionInterval; // 다음 의사 결정 시간 설정
            MakeDecision();
        }

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (currentState != State.KILLED && !isPerformingAction)
        {
            if (distanceToPlayer <= attackRadius)
            {
                // 공격 범위 내에 플레이어가 있는 경우
                animator.SetBool("Walk", false); // Walk 애니메이션 비활성화
                StartCoroutine(RandomAction());
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

        if (currentState == State.PATROL || currentState == State.CHASE)
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
        animator.SetBool("Walk", false); // Walk 애니메이션 비활성화

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
            case State.SKILL:
                Debug.Log("S");
                animator.SetTrigger("Skill");
                break;
            case State.ROLL:
                Debug.Log("R");
                animator.SetTrigger("Roll");
                break;
            case State.BLOCK:
                Debug.Log("B");
                animator.SetTrigger("Block");
                break;
            case State.SUMMONS:
                Debug.Log("S");
                animator.SetTrigger("Summons");
                break;
        }

        yield return new WaitForSeconds(actionDuration); // 행동 애니메이션 시간 대기

        isPerformingAction = false;
        currentState = State.IDLE;
    }

    private IEnumerator RandomAction()
    {
        isPerformingAction = true;
        yield return new WaitForSeconds(2f); // 2초 대기

        float randomValue = Random.value;
        if (randomValue < 0.15f)
        {
            currentState = State.ATTACK1;
            StartCoroutine(PerformAction(State.ATTACK1, 1f));
        }
        else if (randomValue < 0.3f)
        {
            currentState = State.ATTACK2;
            StartCoroutine(PerformAction(State.ATTACK2, 1f));
        }
        else if (randomValue < 0.45f)
        {
            currentState = State.ATTACK3;
            StartCoroutine(PerformAction(State.ATTACK3, 1f));
        }
        else if (randomValue < 0.6f)
        {
            currentState = State.SKILL;
            StartCoroutine(PerformAction(State.SKILL, 2f));
        }
        else if (randomValue < 0.75f)
        {
            currentState = State.ROLL;
            StartCoroutine(PerformAction(State.ROLL, 1f));
        }
        else if (randomValue < 0.9f)
        {
            currentState = State.BLOCK;
            StartCoroutine(PerformAction(State.BLOCK, 1f));
        }
        else
        {
            currentState = State.SUMMONS;
            StartCoroutine(PerformAction(State.SUMMONS, 3f));
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

    private void MakeDecision()
    {
        float randomValue = Random.value;

        // 강화 학습 및 확률 기반 의사 결정 로직
        if (randomValue < 0.1f)
        {
            currentState = State.ATTACK1;
        }
        else if (randomValue < 0.2f)
        {
            currentState = State.ATTACK2;
        }
        else if (randomValue < 0.3f)
        {
            currentState = State.ATTACK3;
        }
        else if (randomValue < 0.4f)
        {
            currentState = State.SKILL;
        }
        else if (randomValue < 0.5f)
        {
            currentState = State.ROLL;
        }
        else if (randomValue < 0.6f)
        {
            currentState = State.BLOCK;
        }
        else if (randomValue < 0.7f)
        {
            currentState = State.SUMMONS;
        }
        else
        {
            // 다른 상태 유지
        }
    }
}
