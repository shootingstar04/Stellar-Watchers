using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mushroom : MonoBehaviour
{
    public enum State
    {
        IDLE,
        PATROL,
        FLEE,
        KILLED
    }

    public State currentState;

    public float detectionRadius = 10f;
    public float attackRadius = 2f;
    public float moveSpeed = 2f;
    public float fleeSpeed = 5f; // FLEE 상태일 때의 이동 속도
    public float patrolDistance = 5f; // 순찰할 거리
    public float fleeGravityScale = 2f; // FLEE 상태일 때의 중력 스케일
    public float normalGravityScale = 1f; // 일반 중력 스케일
    public float fallingAcceleration = 1.5f; // 떨어지는 가속도

    private Rigidbody2D rb;
    private Animator animator;
    private bool facingRight = true;
    private bool isFalling = false;

    // 추가된 부분: 낭떠러지 및 벽 감지를 위한 변수
    public float cliffDetectionDistance = 1f;
    public float wallDetectionDistance = 0.1f;
    public LayerMask groundLayer;

    private Vector2 startPos;
    private Vector2 patrolLeftLimit;
    private Vector2 patrolRightLimit;
    private Transform player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        currentState = State.PATROL;

        startPos = transform.position;
        patrolLeftLimit = new Vector2(startPos.x - patrolDistance, startPos.y);
        patrolRightLimit = new Vector2(startPos.x + patrolDistance, startPos.y);

        rb.gravityScale = normalGravityScale; // 초기 중력 스케일 설정
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
            case State.FLEE:
                Flee();
                break;
            case State.KILLED:
                Killed();
                break;
        }

        // 벽이나 땅의 끝을 감지하면 방향 전환
        if (currentState == State.PATROL)
        {
            if (IsWallAhead() || IsEdgeAhead())
            {
                Flip();
            }
        }

        // 낭떠러지 감지
        if (currentState == State.FLEE && IsEdgeAhead() && !IsGroundDetected())
        {
            isFalling = true;
        }

        // 떨어질 때의 가속도 적용
        if (isFalling)
        {
            rb.velocity += Vector2.down * fallingAcceleration * Time.deltaTime;
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
        MoveTo(target, moveSpeed);
    }

    private void Flee()
    {
        rb.gravityScale = fleeGravityScale; // FLEE 상태에서 중력 스케일 증가
        animator.SetBool("Walk", true);

        float direction = player.position.x > transform.position.x ? -1f : 1f; // 플레이어와 반대 방향으로 도망
        Vector2 moveDirection = new Vector2(direction, 0f);
        rb.velocity = moveDirection * fleeSpeed;
    }

    private void Killed()
    {
        rb.velocity = Vector2.zero;
        animator.SetTrigger("Die");

        // 죽음 처리를 여기서 수행합니다. 예: 파괴, 리스폰 등
    }

    private void MoveTo(Vector2 target, float speed)
    {
        float direction = target.x > transform.position.x ? 1f : -1f;
        Vector2 moveDirection = new Vector2(direction, 0f);
        rb.velocity = moveDirection * speed;
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

    private bool IsGroundDetected()
    {
        Vector2 position = transform.position;
        RaycastHit2D hit = Physics2D.Raycast(position, Vector2.down, 1f, groundLayer);

        Debug.DrawRay(position, Vector2.down * 1f, Color.yellow);

        return hit.collider != null; // 땅을 감지하면 true를 반환
    }

    public void TakeDamage(int damage)
    {
        // 체력 감소 로직을 추가하고 체력이 0 이하가 되면 KILLED 상태로 전환합니다.
        currentState = State.FLEE;
        rb.gravityScale = fleeGravityScale; // FLEE 상태로 전환 시 중력 스케일 증가
        Flip(); // 플레이어와 반대 방향으로 도망가기 위해 Flip
    }
}
