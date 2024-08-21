using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvilWizard : MonoBehaviour
{
    public enum State
    {
        IDLE,
        PATROL,
        CHASE,
        ATTACK,
        KILLED,
        Run
    }

    public State currentState;

    public Transform player;
    public GameObject projectilePrefab; // 원거리 공격에 사용할 투사체 프리팹
    public Transform firePoint; // 투사체 발사 위치
    public float currentHP = 18;
    public float detectionRadius = 10f;
    public float runRadius = 10f;
    public float attackRadius = 5f;
    public float moveSpeed = 2f;
    public float patrolDistance = 5f; // 순찰할 거리
    public float attackCoolTime = 2f;

    private float attackDelay = 1f; // 공격 딜레이

    private Rigidbody2D rb;
    private Animator animator;
    private bool facingRight = true;
    private float attackCount = 0;

    // 추가된 부분: 낭떠러지 및 벽 감지를 위한 변수
    public float cliffDetectionDistance = 1f;
    public float wallDetectionDistance = 0.1f;
    public LayerMask groundLayer;

    private Vector2 startPos;
    private Vector2 patrolLeftLimit;
    private Vector2 patrolRightLimit;

    private bool isPatrollingRight;
    private float patrolTimer;
    private float stopTimer;
    private float flipCooldown = 0.5f; // 방향 전환 쿨다운
    private float flipTimer;
    private bool isPerforming;
    private bool dead = false;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
        rb.freezeRotation = true; // 회전 고정
        animator = GetComponent<Animator>();

        currentState = State.PATROL;

        startPos = transform.position;
        patrolLeftLimit = new Vector2(startPos.x - patrolDistance, startPos.y);
        patrolRightLimit = new Vector2(startPos.x + patrolDistance, startPos.y);

        isPatrollingRight = true;
        patrolTimer = 5f;
        stopTimer = 0f;
    }

    private void Update()
    {
        if (currentState == State.KILLED)
        {
            StopAllCoroutines();
            if (!dead)
            {
                Killed();
                dead = true;
            }
            return;
        }

        if (isPerforming)
        {
            return;
        }

        attackCount += Time.deltaTime;

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
                StartCoroutine(AttackRoutine());
                break;
            case State.Run:
                StartCoroutine(run());
                break;
        }

        // 상태 전환 로직
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (currentState != State.KILLED)
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

            if (distanceToPlayer < runRadius)
            {
                currentState = State.Run;
            }

            if (distanceToPlayer <= attackRadius && attackCount > attackCoolTime)
            {
                attackCount = 0;
                currentState = State.ATTACK;
            }


            if (currentState == State.CHASE && distanceToPlayer > detectionRadius)
            {
                currentState = State.PATROL;
            }
        }

        // 벽이나 땅의 끝을 감지하면 방향 전환
        if (currentState == State.PATROL)
        {
            flipTimer -= Time.deltaTime;

            if ((IsWallAhead() || IsEdgeAhead()) && flipTimer <= 0)
            {
                Flip();
                flipTimer = flipCooldown; // 쿨다운 타이머 리셋
            }
        }
    }

    private IEnumerator run()
    {
        animator.SetBool("Run", true);
        isPerforming = true;
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        while (distanceToPlayer < runRadius + 1)
        {
            distanceToPlayer = Vector2.Distance(transform.position, player.position);

            Debug.Log(distanceToPlayer - runRadius + 1);

            if (player.transform.position.x - this.transform.position.x > 0 && facingRight)
            {
                Flip();
            }
            if (player.transform.position.x - this.transform.position.x < 0 && !facingRight)
            {
                Flip();
            }

            Vector2 speed = new Vector2((facingRight ? 2 : -2) * moveSpeed, rb.velocity.y);

            rb.velocity = speed;
            yield return null;
        }
        animator.SetBool("Run", false);
        Debug.Log("end");
        currentState = State.PATROL;
        isPerforming = false;
    }

    private void Idle()
    {
        animator.SetBool("Walk", false);
        rb.velocity = Vector2.zero;

        stopTimer -= Time.deltaTime;

        if (stopTimer <= 0)
        {
            currentState = State.PATROL;
        }
    }

    private void Patrol()
    {
        patrolTimer -= Time.deltaTime;

        if (patrolTimer <= 0f)
        {
            currentState = State.IDLE;
            stopTimer = 3f;
            patrolTimer = 5f;
        }

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
        if (IsNearCliff())
        {
            Flip();
            currentState = State.PATROL; // 낭떠러지를 피하기 위해 추적을 멈추고 순찰 상태로 변경
        }
    }

    private void Attack()
    {
        if (player.transform.position.x - this.transform.position.x < 0 && facingRight)
        {
            Flip();
        }
        if (player.transform.position.x - this.transform.position.x > 0 && !facingRight)
        {
            Flip();
        }
        rb.velocity = Vector2.zero;
        animator.SetBool("Walk", false);
        animator.SetTrigger("Attack");
    }

    private IEnumerator AttackRoutine()
    {
        isPerforming = true;
        Attack();
        yield return new WaitForSeconds(attackDelay); // 공격 딜레이 만큼 기다림
        currentState = State.IDLE;
        isPerforming = false;
        SpawnProjectile();

    }

    private void Killed()
    {
        rb.velocity = Vector2.zero;
        animator.SetTrigger("Death");
        Destroy(this.GetComponent<EnemyData>());
        Destroy(this.gameObject, 2f);
        Destroy(this);
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

        return hit.collider == null; // 낭떠러지이면 true를 반환
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

    private void SpawnProjectile()
    {
        isPerforming = false;

        GameObject instance = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);

        instance.GetComponent<EnemyProjectile>().dir = facingRight ? 1 : -1;

    }

    public void TakeDamage(float damage)
    {
        GetComponent<ImpulseSource>().ShakeEffect();
        currentHP -= damage;
        if (currentHP < 0)
        {
            rb.gravityScale = 0;
            Destroy(this.GetComponent<CapsuleCollider2D>());
            currentState = State.KILLED;
        }
        else
        {
            animator.SetTrigger("hurt");
            StopAllCoroutines();
            isPerforming = false;
            currentState = State.IDLE;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(this.transform.position, detectionRadius);

        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(this.transform.position, runRadius);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(this.transform.position, attackRadius);
    }
}
