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

            //Vector2 direction = collider.transform.position - circleCenter;

            switch (collider.gameObject.tag)
            {
                case (Define.PlayerTag):
                    {
                        //collider.GetComponent<"PlayerHealth">().Instance.Damaged(bombDamage);
                        var rigid = collider.GetComponent<Rigidbody2D>();
                        if (rigid != null)
                            rigid.AddForce(Vector2.up * 500, ForceMode2D.Impulse);
                        else
                            Debug.Log("ÆøÅº ³Ë¹é ÀÎ½Ä¸øÇÔ");
                    }
                    break;
                case (Define.EnemyTag):
                    {
                        //collider.Instance.Damaged(bombDamage);
                        var rigid = collider.GetComponent<Rigidbody2D>();

                        if (rigid != null)
                        {
                            Debug.Log("ÆøÅº ³Ë¹é ÇÔ");
                            rigid.AddForce(Vector2.up * 500, ForceMode2D.Impulse);
                        }
                        else
                        {
                            Debug.Log("ÆøÅº ³Ë¹é ÀÎ½Ä¸øÇÔ");
                        }

                            
                    }
                    break;
                default:
                    {
                        Debug.Log("collider Å×±× ÀÎ½ÄºÒ°¡");
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
