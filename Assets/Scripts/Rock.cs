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
        if(collision.rigidbody != null && collision.gameObject.CompareTag("Player")) //Ȥ�� ���� ��Ʈ�ڽ��� �ν��ұ�� rigidbody ���������߰�
        {
            Debug.Log("�� ����");
            col.isTrigger = true; //�������
        }
    }
}
