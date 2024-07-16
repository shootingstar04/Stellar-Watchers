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
        if(collision.gameObject.CompareTag("Player")) //Ȥ�� ���� ��Ʈ�ڽ��� �ν��ұ�� rigidbody ���������߰� //������ ���ݹ����� ������Ʈ�� �ƴѰͰ��� rigidbodyüũ ����
        {
            Debug.Log("�� ����");
            Physics2D.IgnoreCollision(collision.collider, col);
        }
    }
}
