using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mushroom : MonoBehaviour
{
    public static Mushroom instance;
    public enum State
    {
        IDLE,
        PATROL,
        FLEE,
        KILLED
    }

    public State currentState;

    public float moveSpeed = 5f;
    public float fleeSpeed = 8f;
    public float patrolDistance = 10f;
    public float fallingAcceleration = 1000f;
    
    public float CurHP = 8;

    private Rigidbody2D rb;
    private Animator animator;
    private bool facingRight = true;
    private bool isFalling = false;

    public float cliffDetectionDistance = 2f;
    public float wallDetectionDistance = 1f;
    public LayerMask groundLayer;

    private Vector2 startPos;
    private Vector2 patrolLeftLimit;
    private Vector2 patrolRightLimit;
    private Transform player;

    void Awake()
    {   
        instance = this;
    }

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        currentState = State.PATROL;

        startPos = transform.position;
        patrolLeftLimit = new Vector2(startPos.x - patrolDistance, startPos.y);
        patrolRightLimit = new Vector2(startPos.x + patrolDistance, startPos.y);
    }

    void Update()
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

        if (currentState == State.PATROL)
        {
            if (IsWallAhead() || IsEdgeAhead())
            {
                Flip();
            }
        }

        if (currentState == State.FLEE && IsEdgeAhead() && !IsGroundDetected())
        {
            isFalling = true;
        }

        if (isFalling)
        {
            rb.velocity += Vector2.down * fallingAcceleration * Time.deltaTime;
        }

        if (CurHP <= 0)
        {
            Killed();
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
        MoveTo(target, moveSpeed);
    }

    private void Flee()
    {
        animator.SetBool("Walk", true);

        float direction = player.position.x > transform.position.x ? -1f : 1f;
        Vector2 moveDirection = new Vector2(direction, 0f);
        rb.velocity = moveDirection * fleeSpeed;
    }

    private void Killed()
    {
        rb.velocity = Vector2.zero;
        animator.SetTrigger("Die");

        StartCoroutine(DieAfterAnimation());
    }

    private IEnumerator DieAfterAnimation()
    {
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        yield return new WaitForSeconds(stateInfo.length);
        animator.speed = 0;
        yield return new WaitForSeconds(1f);

        Destroy(gameObject);
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

    private bool IsGroundDetected()
    {
        Vector2 position = transform.position;
        RaycastHit2D hit = Physics2D.Raycast(position, Vector2.down, 1f, groundLayer);

        Debug.DrawRay(position, Vector2.down * 1f, Color.yellow);

        return hit.collider != null;
    }

    public void TakeDamage(float damage)
    {
        animator.SetTrigger("Hit");
        currentState = State.FLEE;
        CurHP -= damage;

        Flip();
    }
}