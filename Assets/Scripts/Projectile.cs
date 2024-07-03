using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float lifetime = 2f; // 탄환의 수명 (초 단위)

    void Start()
    {
        // 일정 시간 후 탄환 파괴
        Destroy(gameObject, lifetime);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // 충돌 시 탄환 파괴
        Destroy(gameObject);
    }
}
