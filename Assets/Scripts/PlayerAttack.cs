using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public string playerTag; // "Player1" or "Player2"
    public float meleeAttackRange = 2.0f;
    public float meleeAttackDamage = 10.0f;
    public GameObject projectilePrefab;
    public Transform projectileSpawnPoint;
    public float projectileSpeed = 10.0f;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            if (playerTag == "Player1")
            {
                MeleeAttack();
            }
            else if (playerTag == "Player2")
            {
                RangedAttack();
            }
        }
    }

    void MeleeAttack()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, meleeAttackRange))
        {
            // Assuming the enemy has a script with a TakeDamage method
            if (hit.collider.CompareTag("Enemy"))
            {
                hit.collider.GetComponent<Enemy>().TakeDamage(meleeAttackDamage);
            }
        }
        Debug.Log("Player1 performed a melee attack!");
    }

    void RangedAttack()
    {
        GameObject projectile = Instantiate(projectilePrefab, projectileSpawnPoint.position, projectileSpawnPoint.rotation);
        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        rb.velocity = transform.forward * projectileSpeed;
        Debug.Log("Player2 performed a ranged attack!");
    }
}
