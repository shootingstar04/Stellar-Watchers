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

        float d = dir.magnitude; //dir의 길이(벡터길이)
        float o1 = 0.5f; //체력반경
        float p1 = 1.0f; //플레이어 반경

        if (d < o1 + p1)
        {
            Debug.Log("작동");
            Destroy(gameObject);
        }
    }
}
