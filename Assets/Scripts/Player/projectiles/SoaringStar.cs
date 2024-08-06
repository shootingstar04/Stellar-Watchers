using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoaringStar : MonoBehaviour
{
    public Transform bottom;
    public int hitCount = 30;

    private GameObject target = null;

    private float xRange = 0;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(attack());
    }

    void FixedUpdate()
    {
        Debug.Log("a");
        move_to_target();
        Debug.Log("b");
    }

    private void move_to_target()
    {
        if (target != null)
        {
            Vector2 Pos = target.transform.position;
            Pos.y += 3.5f;

            RaycastHit2D Hit = Physics2D.Raycast((Vector2)Pos, Vector2.down, 50, LayerMask.GetMask("Ground"));

            if (Hit.collider != null)
            {
                Pos = Hit.point;
                Pos.y += 3.5f;
            }

            this.transform.position = Pos;
        }
    }

    IEnumerator attack()
    {
        yield return new WaitForSeconds(2);

        for (int i = 0; i < hitCount; i++)
        {
            xRange += 3f / hitCount;

            Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(this.transform.position, new Vector2(xRange, 7), 0);

            foreach (Collider2D collider in collider2Ds)
            {
                if (collider != null)
                {
                    if (collider.CompareTag(Define.EnemyTag))
                    {
                        collider.GetComponent<EnemyData>().TakeDamage(5);
                        Debug.Log(collider.name + " 에게 " + 5 + "의 데미지를 입힘");
                    }
                    else if (collider.tag == "BOMB")
                    {
                        Debug.Log("폭탄 맞음");
                        Bomb.onFire();
                    }
                    else if (collider.GetComponent<ElevatorSwitch>() != null)
                    {
                        Debug.Log("스위치 작동");
                        collider.GetComponent<ElevatorSwitch>().SwitchFlick();
                    }
                    else if (collider.GetComponent<Chest>() != null)
                    {
                        collider.gameObject.GetComponent<Chest>().Distroyed();
                    }
                    else if (collider.GetComponent<GeneralDoor>() != null)
                    {
                        collider.GetComponent<GeneralDoor>().OpenDoor();
                    }
                }
            }

            yield return new WaitForSeconds((float)2 / hitCount);
        }

        Destroy(this.gameObject);
    }

    public void set_target(GameObject enemy)
    {
        target = enemy;
        if (enemy != null)
        {
            Debug.Log("set target, " + enemy.name);

            Vector2 Pos = target.transform.position;
            Pos.y += 3f;

            this.transform.position = Pos;

            move_to_target();
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(this.transform.position, new Vector2(xRange, 7));
    }
}
