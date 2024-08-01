using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireWizard : MonoBehaviour
{
    public enum State
    {
        IDLE,
        ATTACK,
        HIT,
        KILLED
    }

    public State currentState;

    public Transform player;
    public float detectionRadius = 10f;
    public float attackRadius = 7f;
    public float attackCooldown = 5f;

    public float CurHP = 100;

    private Rigidbody2D rb;
    private Animator animator;

    public GameObject[] projectiles; // 5개의 투사체 프리팹을 저장하는 배열

    private bool isAttacking = false;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        currentState = State.IDLE;
    }

    private void Update()
    {
        if (currentState == State.KILLED) return;

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (currentState != State.ATTACK && distanceToPlayer <= attackRadius && !isAttacking)
        {
            StartCoroutine(Attack());
        }
        else if (currentState == State.IDLE && distanceToPlayer > attackRadius && distanceToPlayer <= detectionRadius)
        {
            currentState = State.IDLE; // 플레이어를 인식하지만 아직 공격 범위에 있지 않음
        }
        else if (currentState == State.IDLE && distanceToPlayer > detectionRadius)
        {
            currentState = State.IDLE; // 플레이어가 감지 범위 밖에 있음
        }
    }

    private IEnumerator Attack()
    {
        currentState = State.ATTACK;
        isAttacking = true;

        // 랜덤으로 투사체 발사
        int randomProjectileIndex = Random.Range(0, projectiles.Length);
        GameObject selectedProjectile = projectiles[randomProjectileIndex];
        Instantiate(selectedProjectile, transform.position, Quaternion.identity);

        yield return new WaitForSeconds(attackCooldown);

        currentState = State.IDLE;
        isAttacking = false;
    }

    public void TakeDamage(float damage)
    {
        if (currentState == State.KILLED) return;

        CurHP -= damage;
        animator.SetTrigger("Hit");

        if (CurHP <= 0)
        {
            currentState = State.KILLED;
            animator.SetTrigger("Die");
            // 죽음 처리를 여기서 수행합니다. 예: 파괴, 리스폰 등
        }
        else
        {
            currentState = State.HIT;
        }
    }
}
