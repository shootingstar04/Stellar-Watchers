using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackRanged : MonoBehaviour
{
    private float curTime = 0f;
    public float coolTime = 0.5f;

    public GameObject projectilePrefab;
    public Transform projectileSpawnPoint;
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
    }
}
