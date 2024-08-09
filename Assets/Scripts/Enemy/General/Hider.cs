using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hider : MonoBehaviour
{
    public enum State
    {
        IDLE,
        ATTACK,
        KILLED
    }
    
    public State currentState;
    public Transform player;
    public float detectionRadius = 3f; // 공격을 시작할 범위
    public float attackDelay = 2f; // 공격 간의 딜레이
    public float CurHP = 10;

    private Animator animator;
    private bool isAttacking = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        currentState = State.IDLE;
    }

    void Update()
    {
        switch (currentState)
        {
            case State.IDLE:
                CheckForAttack();
                break;
            case State.ATTACK:
                if (!isAttacking) {
                    StartCoroutine(Attack());
                }
                break;
            case State.KILLED:
                Killed();
                break;
        }
    }

    void CheckForAttack()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        if (distanceToPlayer <= detectionRadius && !isAttacking)
        {
            currentState = State.ATTACK;
        }
    }

    IEnumerator Attack()
    {
        isAttacking = true;
        animator.SetTrigger("Attack");

        // 공격 애니메이션 재생 시간 대기
        yield return new WaitForSeconds(1f);

        // 공격 범위 내의 충돌체 확인
        Vector2 attackCenter = new Vector2(transform.position.x, transform.position.y);
        Collider2D[] hitPlayers = Physics2D.OverlapCircleAll(attackCenter, detectionRadius, LayerMask.GetMask("Player"));

        foreach (var hitPlayer in hitPlayers)
        {
            if (hitPlayer.CompareTag("Player"))
            {
                hitPlayer.GetComponent<PlayerHealth>().modify_HP(-1); // 예: 데미지를 1로 설정
            }
        }

        // 공격 후 딜레이
        yield return new WaitForSeconds(attackDelay);

        isAttacking = false;
        currentState = State.IDLE; // 공격 후 IDLE 상태로 돌아감
    }

    private void Killed()
    {
        this.tag = "Untagged"; //테그도 없애면 되지않을까 <-태그 없앴더니 됨
        animator.SetTrigger("Die");
        Destroy(gameObject, 1f);
    }

    public void TakeDamage(float damage)
    {
        animator.SetTrigger("Hit");
        CurHP -= damage;

        if (CurHP <= 0)
        {
            currentState = State.KILLED;
        }
    }
}
