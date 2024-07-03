using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    private bool isInvincible;
    private float invincibilityCounter;
    public float invincibilityDuration = 1f;
    public Collider2D Pc;
    SpriteRenderer spriteRenderer;

    void Start()
    {
        Pc = this.GetComponent<Collider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Enemy" && !isInvincible)
        {
            Debug.Log("Player hit");
            isInvincible = true;
            invincibilityCounter = invincibilityDuration;
        }
    }
}
