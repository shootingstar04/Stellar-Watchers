using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class Bomb : MonoBehaviour
{
    private List<GameObject> objectsInTrigger = new List<GameObject>();

    private static Vector2 circleCenter;
    private static float circleRadius = 3f;

    private static int bombDamage = 50;

    protected void Awake()
    {
        circleCenter = this.transform.position;
    }


    public static void onFire()
    {

        LayerMask layer = LayerMask.GetMask("Enemy") | LayerMask.GetMask("Player");



        Collider2D[] colliders = Physics2D.OverlapCircleAll(circleCenter, circleRadius, layer);


        if (colliders.Length == 0)

        {
            Debug.Log("½ÇÆÐ");
        }

        foreach (Collider2D collider in colliders)
        {
            Debug.Log("Object inside circle: " + collider.gameObject.name);
            switch (collider.gameObject.tag)
            {
                case (Define.PlayerTag):
                    {
                        //collider.GetComponent<"PlayerHealth">().Instance.Damaged(bombDamage);
                        collider.GetComponent<Rigidbody2D>().AddForce(Vector2.up*10);
                    }
                    break;
                case (Define.EnemyTag):
                    {
                        //collider.Instance.Damaged(bombDamage);
                        collider.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 10);
                    }
                    break;
            }

        }

    }





    void Triggered(Vector2 center, float radius, LayerMask mask, float newEnemyHealth, float newPlayerHealth)
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(center, radius, mask);

        foreach (Collider2D collider in colliders)
        {
            Enemy enemy = collider.GetComponent<Enemy>();
            //Player player = collider.GetComponent<Player>();

            if (enemy != null)
            {
                enemy.health = newEnemyHealth;
                Debug.Log("Changed health of enemy: " + enemy.gameObject.name + " to " + newEnemyHealth);
            }

            /*
            if (player != null)
            {
                player.health = newPlayerHealth;
                Debug.Log("Changed health of player: " + player.gameObject.name + " to " + newPlayerHealth);
            }
            */
        }
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(circleCenter, circleRadius);
    
    }

}
