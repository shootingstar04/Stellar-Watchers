using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float lifetime = 2f;
    public float projectileSpeed = 20f;

    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        if (transform.rotation.y == 0)
        {
            transform.Translate(transform.right * projectileSpeed * Time.deltaTime);
        }
        else
        {
            transform.Translate(transform.right * -1 * projectileSpeed * Time.deltaTime);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}
