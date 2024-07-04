using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{
    private Collider2D col;

    private void Awake()
    {
        col = GetComponent<Collider2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.rigidbody != null && collision.gameObject.CompareTag("Player")) //혹시 공격 히트박스도 인식할까봐 rigidbody 보유조건추가
        {
            Debug.Log("돌 맞음");
            col.isTrigger = true; //이후통과
        }
    }
}
