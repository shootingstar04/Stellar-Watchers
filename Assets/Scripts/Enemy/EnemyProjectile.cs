using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    public float lifetime = 2f;
    public float projectileSpeed = 20f;

    [HideInInspector]
    public int dir = 1;

    private float lifeCounter = 0;
    void Start()
    {
        Rigidbody2D rigid = this.GetComponent<Rigidbody2D>();

        if (dir == -1)
        {
            Vector3 vector3 = this.transform.localScale;
            vector3.x *= -1;
            this.transform.localScale = vector3;
        }

        rigid.velocity = new Vector2(dir * projectileSpeed, 0);
    }

    void Update()
    {
        lifeCounter += Time.deltaTime;
        if (lifeCounter > lifetime)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(Define.PlayerTag))
        {
            Destroy(this.gameObject);
        }
    }
}
