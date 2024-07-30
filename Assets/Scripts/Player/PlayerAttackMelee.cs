using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackMelee : MonoBehaviour
{
    private float curTime = 0f;
    private float coolTime = 0.5f;
    public int Damage = 5;

    public Transform Pos;
    public Transform UpPos;
    public Transform DownPos;
    public Transform SlashPos;
    public Vector2 boxSize1;
    public Vector2 boxSize2;
    public Vector2 boxSize3;

    Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            if (curTime <= 0)
            {
                animator.SetTrigger("Attack1");
                MeleeAttack();
                curTime = coolTime;
            }
        }
        /*if (Input.GetKeyDown(KeyCode.A))
        {
            if (curTime <= 0)
            {
                SlashAttack();
                curTime = coolTime;
            }
        }*/

        if (curTime > 0) curTime -= Time.deltaTime;
    }

    void MeleeAttack()
    {
        Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(Pos.position, boxSize1, 0);

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            collider2Ds = Physics2D.OverlapBoxAll(UpPos.position, boxSize2, 0);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            collider2Ds = Physics2D.OverlapBoxAll(DownPos.position, boxSize2, 0);
        }

        foreach (Collider2D collider in collider2Ds)
        {
            if (collider.tag == "Enemy")
            {
                collider.GetComponent<EnemyData>().TakeDamage(Damage * (ProgressData.Instance.reinforcementCount + 1));
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

    void SlashAttack()
    {
        Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(SlashPos.position, boxSize1, 0); ;

        foreach (Collider2D collider in collider2Ds)
        {
            if (collider.tag == "Enemy")
            {
                Debug.Log("적 맞음");
            }
            else if (collider.tag == "BOMB")
            {
                Debug.Log("폭탄 맞음");
                Bomb.onFire();
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(Pos.position, boxSize1);
        Gizmos.DrawWireCube(UpPos.position, boxSize2);
        Gizmos.DrawWireCube(DownPos.position, boxSize2);
        Gizmos.DrawWireCube(SlashPos.position, boxSize3);
    }
}
