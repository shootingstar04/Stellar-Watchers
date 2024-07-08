using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : MonoBehaviour
{
    public Transform SensingPos;
    public Transform HitPos;
    public Vector2 boxSize1;
    public Vector2 boxSize2;
    
    Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        Hit();
        Sensing();
    }

    void Hit()
    {
        Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(HitPos.position, boxSize2, 0);;

        foreach(Collider2D collider in collider2Ds)
        {
            if (collider.tag == "Player")
            {
                animator.SetTrigger("Attack1");
                Debug.Log("적 공격");
            }
        }
    }

    void Sensing()
    {
        Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(SensingPos.position, boxSize1, 0);;

        foreach(Collider2D collider in collider2Ds)
        {
            if (collider.tag == "Player")
            {
                Debug.Log("적 감지");
            }
        }
    }

    private void OnDrawGizmos() 
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(SensingPos.position, boxSize1);
        Gizmos.DrawWireCube(HitPos.position, boxSize2);
    }
}
