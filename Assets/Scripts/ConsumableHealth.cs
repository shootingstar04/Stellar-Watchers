using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class ConsumableHealth : MonoBehaviour
{
    private GameObject Player;

    // Start is called before the first frame update
    void Start()
    {
        this.Player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        Vector2 o = transform.position;
        Vector2 p = this.Player.transform.position;
        Vector2 dir = o - p;

        float d = dir.magnitude; //dir�� ����(���ͱ���)
        float o1 = 0.5f; //ü�¹ݰ�
        float p1 = 1.0f; //�÷��̾� �ݰ�

        if (d < o1 + p1)
        {
            Debug.Log("�۵�");
            Destroy(gameObject);
        }
    }
}
