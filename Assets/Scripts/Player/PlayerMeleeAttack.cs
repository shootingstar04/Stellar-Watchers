using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMeleeAttack : MonoBehaviour
{
    private float curTime = 0f;
    public float coolTime = 0.5f;

    public Transform Pos;
    public Transform UpPos;
    public Transform DownPos;
    public Vector2 boxSize1;
    public Vector2 boxSize2;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            if (curTime <= 0) 
            {
                MeleeAttack();
                curTime = coolTime;
            }
        }
        curTime -= Time.deltaTime;
    }

    void MeleeAttack()
    {
        Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(Pos.position, boxSize1, 0);;

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            collider2Ds = Physics2D.OverlapBoxAll(UpPos.position, boxSize2, 0);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            collider2Ds = Physics2D.OverlapBoxAll(DownPos.position, boxSize2, 0);
        }

        foreach(Collider2D collider in collider2Ds)
        {
            if (collider.tag == "Enemy")
            {
                Debug.Log("적 맞음");
            }
        }
    }

    private void OnDrawGizmos() 
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(Pos.position, boxSize1);
        Gizmos.DrawWireCube(UpPos.position, boxSize2);
        Gizmos.DrawWireCube(DownPos.position, boxSize2);
    }
}