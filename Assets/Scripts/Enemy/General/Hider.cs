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
    public float detectionRadius = 10f;
    public float attackDelay = 2f;
    public LayerMask playerLayer;

    private Animator animator;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        animator = GetComponent<Animator>();
        currentState = State.IDLE;
    }

    private void Update()
    {
        switch (currentState)
        {
            case State.IDLE:
                Idle();
                break;
            case State.ATTACK:
                // 공격 상태에서는 별도로 처리할 필요 없음 (코루틴으로 처리)
                break;
            case State.KILLED:
                Killed();
                break;
        }

        if (currentState != State.KILLED)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, player.position);
            if (distanceToPlayer <= detectionRadius && currentState == State.IDLE)
            {
                currentState = State.ATTACK;
                StartCoroutine(AttackPlayer());
            }
        }
    }

    private void Idle()
    {
        animator.SetBool("Walk", false);
        // 가만히 있는 상태
    }

    private IEnumerator AttackPlayer()
    {
        // 2초 대기
        yield return new WaitForSeconds(attackDelay);

        // 공격 애니메이션 트리거
        animator.SetTrigger("Attack1");

        // 여기서 실제 공격 로직을 추가할 수 있습니다. 예: 플레이어에게 데미지를 입히는 함수 호출 등

        // 다시 IDLE 상태로 전환
        currentState = State.IDLE;
    }

    private void Killed()
    {
        animator.SetTrigger("Die");
        // 죽음 처리를 여기서 수행합니다. 예: 파괴, 리스폰 등
    }

    public void TakeDamage(int damage)
    {
        // 체력 감소 로직을 추가하고 체력이 0 이하가 되면 KILLED 상태로 전환합니다.
        currentState = State.KILLED;
    }
}
