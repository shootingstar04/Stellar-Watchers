using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRangedAttack : MonoBehaviour
{
    private Rigidbody2D rigid;
    private float curTime = 0f;
    public float coolTime = 0.5f;

    public GameObject projectilePrefab;
    public Transform projectileSpawnPoint;
    public float projectileSpeed = 20f;

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            if (curTime <= 0) 
            {
                RangedAttack();
                curTime = coolTime;
            }
        }
        curTime -= Time.deltaTime;
    }

    void RangedAttack()
    {
        GameObject projectile = Instantiate(projectilePrefab, projectileSpawnPoint.position, projectileSpawnPoint.rotation);
        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        rigid.velocity = projectileSpawnPoint.right * projectileSpeed;
    }
}
