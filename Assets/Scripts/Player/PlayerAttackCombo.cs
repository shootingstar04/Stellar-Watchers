using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackCombo : MonoBehaviour
{
    public static PlayerAttackCombo instance;
    
    private Rigidbody2D rigid;
    public enum PlayerType { Player1, Player2 }
    public PlayerType playerType;

    private bool xKeyPressed = false;
    private float xKeyPressTime;

    private float AttackTimeCounter;
    
    void Awake()
    {   
        instance = this;
    }

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            xKeyPressed = true;
            xKeyPressTime = 0f;
        }  

        xKeyPressTime += Time.deltaTime;

        if (xKeyPressed && xKeyPressTime > 2f)
        {
            xKeyPressed = false;
        }
    }

    public void ComboInvocation()
    {
        if (xKeyPressed && xKeyPressTime <= 2f)
        {
            if (playerType == PlayerType.Player1)
            {
                PerformRangedAttack();
            }
            else if (playerType == PlayerType.Player2)
            {
                PerformMeleeAttack();
            }

            xKeyPressed = false;
        }
    }

    void PerformRangedAttack()
    {
        Debug.Log("Player1 performs a ranged attack!");
    }

    void PerformMeleeAttack()
    {
        rigid.velocity = new Vector2(15, rigid.velocity.y);

        gameObject.layer = 8;
        AttackTimeCounter -= Time.deltaTime;
        if (AttackTimeCounter <= 0)
        {
            gameObject.layer = 7;
        }
    }
}
