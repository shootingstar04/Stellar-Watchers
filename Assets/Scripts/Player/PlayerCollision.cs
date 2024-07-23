using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    private bool isInvincible;
    private float invincibilityCounter;
    private float invincibilityDuration = 1f;
    public Collider2D Pc;
    SpriteRenderer spriteRenderer;

    void Start()
    {
        Pc = this.GetComponent<Collider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        HandleInvincibility();
    }
    
    void HandleInvincibility()
    {
        if (isInvincible)
        {
            gameObject.layer = 8;
            spriteRenderer.color = new Color(1, 1, 1, 0.4f);

            invincibilityCounter -= Time.deltaTime;
            if (invincibilityCounter <= 0)
            {
                isInvincible = false;
                gameObject.layer = 7;
                spriteRenderer.color = new Color(1, 1, 1, 1);
            }
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Enemy" && !isInvincible)
        {
            Debug.Log("Player hit");
            PlayerHealth.instance.modify_HP(-1);
            isInvincible = true;
            invincibilityCounter = invincibilityDuration;
        }
    }
}
