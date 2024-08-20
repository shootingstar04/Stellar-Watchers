using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;

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
        KILLED,
        DIE
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

    public int coinYield;
    public int spYield;

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
        // KILLED 상태일 경우 다른 상태로 바뀌지 않도록 함
        if (currentState == State.KILLED || currentState == State.DIE)
        {
            Debug.Log("Death");
            return;
        }

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

        if (currentState != State.SHIELD)
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
        float direction = player.position.x > transform.position.x ? 1f : -1f;
    
        // 플레이어 방향으로 이동
        Vector2 moveDirection = new Vector2(direction, 0f);
        rb.velocity = moveDirection * moveSpeed;
        animator.SetBool("Walk", true);

        // 플레이어를 바라보도록 수정
        if ((direction > 0 && !facingRight) || (direction < 0 && facingRight))
        {
            Flip();
        }

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

        // 공격 애니메이션 재생 시간 대기
        yield return new WaitForSeconds(1f);

        // 공격 범위 내의 충돌체 확인
        float offsetX = facingRight ? attackRadius / 2 : -attackRadius / 2;
        Vector2 attackCenter = new Vector2(transform.position.x + offsetX, transform.position.y);
        Collider2D[] hitPlayers = Physics2D.OverlapBoxAll(attackCenter, new Vector2(attackRadius, attackRadius), 0, LayerMask.GetMask("Player"));

        foreach (var hitPlayer in hitPlayers)
        {
            if (hitPlayer.CompareTag("Player"))
            {
                hitPlayer.GetComponent<PlayerHealth>().modify_HP(-1); // 예: 데미지를 1로 설정
            }
        }

        // 공격 후 1초 딜레이
        yield return new WaitForSeconds(1f);

        isAttacking = false;

        // 플레이어가 공격 범위를 벗어난 경우 추격 상태로 전환
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
        GetComponent<GetSOindex>().returnBool();
        this.tag = "Untagged"; //테그도 없애면 되지않을까 <-태그 없앴더니 됨
        animator.SetTrigger("Die");
        EnemyItemDrop drop = this.gameObject.GetComponent<EnemyItemDrop>();
        if (drop == null) Debug.Log("nocomponent");
        else if(currentState != State.DIE)drop.DropCoins(coinYield, spYield);
        Destroy(gameObject, 1f);
        currentState = State.DIE;
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
        if (IsFacingPlayer() && Random.value < 0.1f && currentState != State.KILLED) // 10% 확률로 방어 행동
        {
            currentState = State.SHIELD;
        }
        else
        {
            animator.SetTrigger("Hit");
            Debug.Log("스켈레톤 피격흔들림");
            GetComponent<ImpulseSource>().ShakeEffect();
            GetComponent<AudioSource>().Play(); 
            CurHP -= damage;
        }

        if (CurHP <= 0)
        {
            Invoke("Killed", 0.5f);
            currentState = State.KILLED;
        }
    }

    private bool IsFacingPlayer()
    {
        Vector2 directionToPlayer = player.position - transform.position;
        return (facingRight && directionToPlayer.x > 0) || (!facingRight && directionToPlayer.x < 0);
    }
    

    private void DropCoins()
    {
        int coins = 0;
        Queue<int> CoinNumQueue = new Queue<int>();
        CoinNumQueue.Enqueue(6);

        while (CoinNumQueue.Count > 0)
        {
            coins += 1;
            var count = CoinNumQueue.Dequeue();

            for (int i = 0; i < count; i++)
            {
                var (q, obj) = WhatCoinCurrently(coins);
                var Coin = CoinPool.GetObject(q, obj);

                Coin.transform.position = this.transform.position;

                float rotation = Random.Range(-90f, 90f);
                Coin.transform.Rotate(0f, 0f, rotation);

                var Xdirection = Random.Range(-1f, 1f);
                var Ydirection = 1f;
                Vector2 dir = new Vector2(Xdirection, Ydirection).normalized;
                float force = Random.Range(100f, 300f);
                Coin.GetComponent<Rigidbody2D>().AddForce(dir * force);
            }

        }
    }
    private (Queue<Coin>, GameObject) WhatCoinCurrently(int coin)
    {
        switch (coin)
        {
            case 1:
                return (CoinPool.Instance.poolCoin1Queue, CoinPool.Instance.Coin1);
            case 2:
                return (CoinPool.Instance.poolCoinXQueue, CoinPool.Instance.CoinX);
            case 3:
                return (CoinPool.Instance.poolCoinXVQueue, CoinPool.Instance.CoinXV);

        }
        return (null, null);

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        float offsetX = facingRight ? attackRadius / 2 : -attackRadius / 2;
        Vector2 attackCenter = new Vector2(transform.position.x + offsetX, transform.position.y);
        Gizmos.DrawWireCube(attackCenter, new Vector3(attackRadius, attackRadius, 0));
    }
}
