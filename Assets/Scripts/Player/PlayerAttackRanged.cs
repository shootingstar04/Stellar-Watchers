using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackRanged : MonoBehaviour
{
    private float curTime = 0f;
    public float coolTime = 2f / 3f;

    public GameObject projectilePrefab;
    public Transform projectileSpawnPoint;

    private PlayerMove playerMove;

    Animator animator;

    private void Awake()
    {
        playerMove = this.GetComponent<PlayerMove>();
    }

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X) || Input.GetButtonDown("attack"))
        {
            if (curTime > coolTime)
            {
                animator.SetTrigger("Attack2");
                RangedAttack();
                curTime = 0;
            }
        }
        if (curTime <= coolTime)
        {
            curTime += Time.deltaTime;
        }
    }

    void RangedAttack()
    {

        GameObject instance = Instantiate(projectilePrefab, projectileSpawnPoint.position, projectileSpawnPoint.rotation);

        instance.GetComponent<Projectile>().dir = (int)playerMove.LastDirection;
    }
}
