using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bandit : MonoBehaviour
{
    public enum State
    {
        IDLE,
        CHASE,
        ATTACK,
        KILLED,
        RECOVER
    }

    public State currentState;

    public Transform player;
    public float detectionRadius = 10f;
    public float attackRadius = 2f;
    public float moveSpeed = 4f;
    public float recoverTime = 5f;

    public float CurHP = 20;
    private float maxHP = 20;

    private Rigidbody2D rb;
    private Animator animator;

    private bool isRecovering = false;
    private bool isFullyKilled = false;
    private bool isAttacking = false;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        maxHP = CurHP;
        currentState = State.IDLE;
    }

    private void Update()
    {
        if (isFullyKilled || isAttacking) return;

        switch (currentState)
        {
            case State.IDLE:
                Idle();
                break;
            case State.CHASE:
                Chase();
                break;
            case State.ATTACK:
                StartCoroutine(Attack());
                break;
            case State.RECOVER:
                break;
        }

        if (currentState != State.KILLED && !isRecovering)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, player.position);

            if (distanceToPlayer <= attackRadius)
            {
                currentState = State.ATTACK;
            }
            else if (distanceToPlayer <= detectionRadius)
            {
                currentState = State.CHASE;
            }
            else
            {
                currentState = State.IDLE;
            }
        }
    }

    private void Idle()
    {
        animator.SetBool("Walk", false);
        rb.velocity = Vector2.zero;
    }

    private void Chase()
    {
        animator.SetBool("Walk", true);

        Vector2 direction = (player.position - transform.position).normalized;
        rb.velocity = new Vector2(direction.x * moveSpeed, rb.velocity.y);
    }

    private IEnumerator Attack()
    {
        isAttacking = true;
        rb.velocity = Vector2.zero;
        animator.SetBool("Walk", false);
        animator.SetTrigger("Attack");

        yield return new WaitForSeconds(1f);

        isAttacking = false;

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        if (distanceToPlayer > attackRadius)
        {
            currentState = State.CHASE;
        }
        else
        {
            currentState = State.IDLE;
        }
    }

    public void TakeDamage(float damage)
    {
        animator.SetTrigger("Hurt");
        if (isFullyKilled) return;

        CurHP -= damage;

        if (CurHP <= 0)
        {
            if (!isRecovering)
            {
                StartCoroutine(Recover());
            }
            else
            {
                Die();
            }
        }
    }

    private IEnumerator Recover()
    {
        isRecovering = true;
        currentState = State.KILLED;
        animator.SetTrigger("Die");

        yield return new WaitForSeconds(1f);

        currentState = State.RECOVER;
        animator.SetTrigger("Recover");
        
        yield return new WaitForSeconds(recoverTime);

        CurHP = maxHP;
        currentState = State.IDLE;
        isRecovering = false;
    }

    private void Die()
    {
        currentState = State.KILLED;
        isFullyKilled = true;
        animator.SetTrigger("Die");
        
        Destroy(gameObject, 1f);
    }
}
