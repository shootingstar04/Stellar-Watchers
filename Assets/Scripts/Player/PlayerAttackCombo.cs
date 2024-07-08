using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackCombo : MonoBehaviour
{
    public static PlayerAttackCombo instance;
    public enum PlayerType { Player1, Player2 }
    public PlayerType playerType;

    private bool xKeyPressed = false;
    private float xKeyPressTime;
    
    void Awake()
    {   
        instance = this;
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
        if (xKeyPressTime <= 2f)
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
        Debug.Log("Player2 performs a melee attack!");
    }
}
