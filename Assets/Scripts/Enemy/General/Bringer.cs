using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bringer : MonoBehaviour
{
    public enum State
    {
        IDLE,
        ATTACK1,
        ATTACK2,
        KILLED
    }

    public State currentState;

    public Transform player;
    public float detectionRadius = 10f;
    public float attackRadius = 2f;

    public float CurHP = 20;

    private Animator animator;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        animator = GetComponent<Animator>();

        currentState = State.IDLE;
    }

    private void Update()
    {
        if (currentState == State.KILLED) return;

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        switch (currentState)
        {
            case State.IDLE:
                if (distanceToPlayer <= detectionRadius)
                {
                    currentState = State.ATTACK1;
                }
                break;

            case State.ATTACK1:
                Attack1();
                if (distanceToPlayer <= attackRadius)
                {
                    currentState = State.ATTACK2;
                }
                break;

            case State.ATTACK2:
                Attack2();
                break;
        }
    }

    private void Attack1()
    {
        animator.SetTrigger("Attack1");
        // Attack1 상태에 필요한 행동을 여기 추가할 수 있습니다.
    }

    private void Attack2()
    {
        animator.SetTrigger("Attack2");
        // Attack2 상태에 필요한 행동을 여기 추가할 수 있습니다.
    }

    public void TakeDamage(float damage)
    {
        animator.SetTrigger("Hit");
        CurHP -= damage;

        if (CurHP <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        currentState = State.KILLED;
        animator.SetTrigger("Die");

        Destroy(gameObject, 1f);
    }
}
